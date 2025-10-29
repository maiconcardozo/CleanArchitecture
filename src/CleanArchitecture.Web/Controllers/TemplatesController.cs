using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Templates.Commands.CreateTemplate;
using CleanArchitecture.Application.Templates.Commands.UpdateTemplate;
using CleanArchitecture.Application.Templates.Commands.DeleteTemplate;
using CleanArchitecture.Application.Templates.Queries.GetTemplate;
using CleanArchitecture.Application.Templates.Queries.GetTemplates;
using CleanArchitecture.Application.Templates.DTOs;

namespace CleanArchitecture.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all templates
    /// </summary>
    /// <returns>List of templates</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TemplateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TemplateDto>>> GetAll()
    {
        var query = new GetTemplatesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a specific template by ID
    /// </summary>
    /// <param name="id">Template ID</param>
    /// <returns>Template details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TemplateDto>> GetById(int id)
    {
        var query = new GetTemplateQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    /// <summary>
    /// Creates a new template
    /// </summary>
    /// <param name="command">Template creation data</param>
    /// <returns>Created template</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TemplateDto>> Create([FromBody] CreateTemplateCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing template
    /// </summary>
    /// <param name="id">Template ID</param>
    /// <param name="command">Template update data</param>
    /// <returns>Updated template</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TemplateDto>> Update(int id, [FromBody] UpdateTemplateCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a template
    /// </summary>
    /// <param name="id">Template ID</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteTemplateCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
