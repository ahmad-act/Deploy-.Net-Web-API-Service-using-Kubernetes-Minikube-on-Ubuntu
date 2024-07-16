﻿using AutoMapper;
using BookInformationService.BusinessLayer;
using BookInformationService.DTOs;
using BookInformationService.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace BookInformationService.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class BookInformationController : Controller
    {
        private readonly ILogger<object> _logger;
        private readonly IBookInformationBL _bookInfoBL;


        public BookInformationController(ILogger<object> logger, IBookInformationBL bookBL)
        {
            _logger = logger;
            _bookInfoBL = bookBL;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all book informations")]
        [SwaggerResponse(200, "Successfully retrieved book informations", typeof(List<BookInformation>))]
        [SwaggerResponse(404, "Book Informations not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetBookInformations()
        {
            Log.Information("Entered: GetBookInformations()");

            try
            {
                List<BookInformationDisplayDto>? bookInformationsDisplayDto = await _bookInfoBL.GetBookInformations();

                if (bookInformationsDisplayDto == null || bookInformationsDisplayDto.Count == 0)
                {
                    return NotFound("Book Informations not found");
                }

                return Ok(bookInformationsDisplayDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to complete GetBookInformations(). \nHTTP status code: 500 \nError: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a book information by ID")]
        [SwaggerResponse(200, "Successfully retrieved the book information", typeof(BookInformation))]
        [SwaggerResponse(404, "Book not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetBookInformation(int id)
        {
            Log.Information("Entered: GetBookInformation(int id)");

            try
            {
                var bookInformationDisplayDto = await _bookInfoBL.GetBookInformation(id);

                if (bookInformationDisplayDto == null)
                {
                    return NotFound("Book not found");
                }

                return Ok(bookInformationDisplayDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to complete GetBookInformation(). \nHTTP status code: 500 \nError: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new book information")]
        [SwaggerResponse(201, "Successfully created the book information", typeof(BookInformation))]
        [SwaggerResponse(400, "Invalid book data")]
        [SwaggerResponse(500, "Internal server error occurred")]
        public async Task<IActionResult> CreateBookInformations([FromBody] BookInformationUpdateDto bookInformationUpdateDto)
        {
            Log.Information("Entered: CreateBookInformations([FromBody] BookInformationUpdateDto bookInformationUpdateDto)");

            try
            {
                if (bookInformationUpdateDto == null)
                {
                    return BadRequest("Invalid book data");
                }

                BookInformationDisplayDto? createdBookInformation = await _bookInfoBL.CreateBookInformation(bookInformationUpdateDto);

                if (createdBookInformation == null)
                {
                    return BadRequest("Book information is not created.");
                }

                return Ok(createdBookInformation);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to complete CreateBookInformations(). \nHTTP status code: 500 \nError: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a book information by ID")]
        [SwaggerResponse(200, "Successfully updated the book information")]
        [SwaggerResponse(404, "Book not found")]
        [SwaggerResponse(400, "Invalid book information data")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateBookInformations(int id, [FromBody] BookInformationUpdateDto bookInformationUpdateDto)
        {
            Log.Information("Entered: UpdateBookInformations(int id, [FromBody] BookInformationUpdateDto bookInformationUpdateDto)");

            try
            {
                if (bookInformationUpdateDto == null)
                {
                    return BadRequest("Invalid book data");
                }

                var existingBook = await _bookInfoBL.UpdateBookInformation(id, bookInformationUpdateDto);

                if (existingBook == null)
                {
                    return NotFound("Book not found");
                }

                return Ok(existingBook);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to complete GetBookInformations(). \nHTTP status code: 500 \nError: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a book information by ID")]
        [SwaggerResponse(204, "Successfully deleted the book information", typeof(BookInformation))]
        [SwaggerResponse(404, "Book not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteBookInformations(int id)
        {
            Log.Information("Entered: DeleteBookInformations(int id)");

            try
            {
                string msg = await _bookInfoBL.DeleteBookInformation(id);

                if (!string.IsNullOrEmpty(msg))
                {
                    return NotFound(msg);
                }

                return Ok("Successfully deleted the book information");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to complete DeleteBookInformations(). \nHTTP status code: 500 \nError: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search book information by Title")]
        [SwaggerResponse(200, "Successfully searched the book informations", typeof(List<BookInformation>))]
        [SwaggerResponse(204, "The search did not yield any results")]
        [SwaggerResponse(400, "Please enter search data")]
        [SwaggerResponse(404, "No book informations found matching the search term")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> SearchBookInformations(string? searchTerm)
        {
            Log.Information("Entered: SearchBookInformations(string? searchTerm)");

            try
            {

                if (string.IsNullOrEmpty(searchTerm))
                {
                    return BadRequest("Please enter search data");
                }

                var bookInformations = await _bookInfoBL.SearchBookInformations(searchTerm);

                if (bookInformations == null || bookInformations.Count == 0)
                {
                    return NotFound("No book informations found matching the search term");
                }

                return Ok(bookInformations);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to complete SearchBooks(). \nHTTP status code: 500 \nError: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
