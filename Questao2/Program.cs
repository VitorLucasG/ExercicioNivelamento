using System.Net.Http.Headers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.IO;

public class Program
{
    static HttpClient client = new HttpClient();


    public class Response
    {
        public int page {  get; set; }
        public int per_pagr { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Match> data { get; set; }
    }

    public class Match
    {
        public string Competition { get; set; }
        public int Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Team1Goals { get; set; }

        public int Team2Goals { get; set; }
    }
    


    static async Task<List<Match>> GetMatchResultsByYearAndTeam(string url, int year, string team1= "", string team2 = "")
    {
        var results = new Response();
        int page;
        int totalPages = 0;

        var finalPath = $"{url}?year={year}";

        if(!string.IsNullOrEmpty(team1))
        {
            finalPath += $"&team1={team1}";
        }

        if (!string.IsNullOrEmpty(team2))
        {
            finalPath += $"&team2={team2}";
        }

        HttpResponseMessage response = await client.GetAsync(finalPath);
        if (response.IsSuccessStatusCode)
        {
            results = await response.Content.ReadFromJsonAsync<Response>();
        }

        totalPages = results.total_pages;

        for (page = 2 ; page <= totalPages; page++)
        {
            var pageString = $"&page={page}";
            response = await client.GetAsync(finalPath + pageString);
            var resultPage = new Response();
            if (response.IsSuccessStatusCode)
            {
                resultPage = await response.Content.ReadFromJsonAsync<Response>();
            }
            results.data.AddRange(resultPage.data);
        }

        return results.data;
    }

    public static void Main()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    static async Task RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("https://jsonmock.hackerrank.com/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {

            string teamName = "Paris Saint-Germain";
            int year = 2013;
            int totalGoals = await getTotalScoredGoals(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            teamName = "Chelsea";
            year = 2014;
            totalGoals = await getTotalScoredGoals(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            // Output expected:
            // Team Paris Saint - Germain scored 109 goals in 2013
            // Team Chelsea scored 92 goals in 2014
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        var resultTeam1 = await GetMatchResultsByYearAndTeam("api/football_matches", year, team1:team);
        var resultTeam2 = await GetMatchResultsByYearAndTeam("api/football_matches", year, team2:team);

        int totalGoals = resultTeam1.Select(s => s.Team1Goals).Sum();
        totalGoals += resultTeam2.Select(s => s.Team2Goals).Sum();

        return totalGoals;
    }

}