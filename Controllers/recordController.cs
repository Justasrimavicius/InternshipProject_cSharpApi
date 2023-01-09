using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers; 

// main routing file

[Controller]
[Route("api/[controller]")]
public class RecordController: Controller {
    
    private readonly MongoDBService _mongoDBService;

    public RecordController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    // returns all documents from mongoDB for the main page
    [HttpGet("all")]
    public async Task<List<Record>> Get(){
        return await _mongoDBService.GetAsync();
    }

    // returns a specific ID document for detailed view(ID is in URL params)
    [HttpGet("{id}")]
    public async Task<Record> GetOne(string id){
        return await _mongoDBService.GetOneAsync(id);
    }

    // post of a single document
    [HttpPost]

    public async Task<IActionResult> Post([FromBody] Record Record) {
        await _mongoDBService.CreateAsync(Record);
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        return CreatedAtAction(nameof(Get), new { id = Record.Id }, Record);
    }

}