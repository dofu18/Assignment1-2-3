using Assignment.Data.Models;
using Assignment.Service;
using Assignment.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Assignment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _orderService.GetAllAsync();
        return StatusCode(result.Status, result);
    }

    [Authorize(Roles = "Admin|User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetById(id);
        return StatusCode(result.Status, result);
    }

    [Authorize(Roles = "Admin|User")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderCreateDto order)
    {
        var result = await _orderService.Save(order);
        return StatusCode(result.Status, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _orderService.DeleteById(id);
        return StatusCode(result.Status, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Odata")]
    [EnableQuery]
    public IActionResult GetOrders()
    {
        var orders = _orderService.GetAllOdata();
        return Ok(orders);
    }
}
