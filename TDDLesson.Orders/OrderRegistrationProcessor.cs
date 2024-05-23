namespace TDDLesson;

/*
 * 
 */

public class OrderRegistrationProcessor
{
    private readonly IRepository _orderRepository;

    public OrderRegistrationProcessor(IRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task RegisterOrder(OrderDto dto)
    {
        
    }
}