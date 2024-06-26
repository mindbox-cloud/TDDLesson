﻿namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;

    public AccreditationProposalProcessor(IRevenueService revenueService, IRepository orderRepository)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        
    }
}