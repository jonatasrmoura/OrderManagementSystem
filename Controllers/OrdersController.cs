using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;
using Azure.Messaging.ServiceBus;
using AutoMapper;
using OrderManagementSystem.DTOS;

namespace OrderManagementSystem.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderRepository orderRepository, IMapper mapper, ILogger<OrdersController> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponseDTO>> GetById(Guid id)
    {
        var order = await _orderRepository.FindByIdAsync(id);

        if (order == null)
        {
            return NotFound("Pedido não encontrado");
        }

        var orderDto = _mapper.Map<OrderResponseDTO>(order);

        return Ok(orderDto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDTO>> Create([FromBody] CreateOrderDTO orderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var order = new Order
            {
                Client = orderDto.Client,
                Product = orderDto.Product,
                Value = orderDto.Value,
            };

            var createdOrder = await _orderRepository.CreateAsync(order);

            var responseDto = _mapper.Map<OrderResponseDTO>(createdOrder);

            await SendMessageToServiceBus(createdOrder);

            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao criar pedido");
            return StatusCode(500, "Erro ao criar pedido");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<List<OrderResponseDTO>>>> List()
    {
        var orders = await _orderRepository.FindAllAsync();
        var ordersDto = _mapper.Map<List<OrderResponseDTO>>(orders);
        return Ok(ordersDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderResponseDTO>> Update([FromBody] UpdateOrderDTO orderDto, Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var order = await _orderRepository.FindByIdAsync(id);

            if (order == null)
            {
                return NotFound("Pedido não encontrado");
            }

            order.Client = orderDto.Client;
            order.Product = orderDto.Product;
            order.Value = orderDto.Value;
            order.Status = orderDto.Status;

            var updatedOrder = await _orderRepository.UpdateAsync(order);

            var responseDto = _mapper.Map<OrderResponseDTO>(updatedOrder);

            await SendMessageToServiceBus(updatedOrder);

            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao atualizar pedido");
            return StatusCode(500, "Erro ao atualizar pedido");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderResponseDTO>> Delete(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var order = await _orderRepository.FindByIdAsync(id);

            if (order == null)
            {
                return NotFound("Pedido não encontrado");
            }

            var deletedOrder = await _orderRepository.DeleteAsync(order);

            return Ok(deletedOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao atualizar pedido");
            return StatusCode(500, "Erro ao atualizar pedido");
        }
    }

    // IMPORTANTE: A implementação do Azure Service Bus está configurada mas não foi testada
    // devido à necessidade de assinatura paga. O código segue as boas práticas de implementação.
    private async Task SendMessageToServiceBus(Order order)
    {
        try
        {
            // Código de envio mantido para demonstração
            var connectionString = _configuration.GetValue<string>("AzureServiceBus:ConnectionString");
            var queueName = _configuration.GetValue<string>("AzureServiceBus:QueueName");

            await using var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender(queueName);

            var message = new ServiceBusMessage(order.Id.ToString());
            await sender.SendMessageAsync(message);

            // Comente o que aconteceria na implementação real
            Console.WriteLine("[SIMULAÇÃO] Mensagem enviada para o Service Bus - OrderId: " + order.Id);
        }
        catch (Exception ex)
        {
            // Simula o comportamento esperado
            Console.WriteLine($"[SIMULAÇÃO] Erro ao enviar para Service Bus: {ex.Message}");
        }
    }
}