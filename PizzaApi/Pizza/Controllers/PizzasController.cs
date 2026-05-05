using Microsoft.AspNetCore.Mvc;
using PizzaApi.Models;

namespace PizzaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzasController : ControllerBase
{
    private static List<Pizza> pizzas = new()
    {
        new Pizza { Id = 1, Name = "Margherita" },
        new Pizza { Id = 2, Name = "Pepperoni" },
        new Pizza { Id = 3, Name = "Calabresa" } 
    };

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => Ok(pizzas);

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza == null) return NotFound();
        return Ok(pizza);
    }

    [HttpPost]
    public ActionResult<Pizza> Create(Pizza pizza)
    {
        pizza.Id = pizzas.Max(p => p.Id) + 1;
        pizzas.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza updatedPizza)
    {
        var pizza = pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza == null) return NotFound();

        pizza.Name = updatedPizza.Name;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza == null) return NotFound();

        pizzas.Remove(pizza);
        return NoContent();
    }
}