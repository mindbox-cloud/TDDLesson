using TDDLesson.Responses;

namespace TDDLesson;

public sealed record ProcessedProposal(ProposalDto ProposalDto, DateTime ProcessedDateTime, ProposalStatus Status);