using EasyBib.Domain;
using EasyBib.Domain.Enums;

namespace EasyBib.WebApi.Models;

// ============================================================
// DTOs (Application-Layer, bewusst von den Entitäten getrennt)
// ============================================================
public record MemberDto(Guid Id, string Name, string Email);
public record CreateMemberRequest(string Name, string Email, MembershipPlan? Plan);
public record MediaItemDto(Guid Id, string Title, string EAN, MediaType Type);
public record CreateMediaItemRequest(string Title, string EAN, MediaType Type);
public record LoanDto(Guid Id, Guid MembershipId, Guid MediaItemId, LoanStatus Status, DateOnly DueDate);
public record CheckOutRequest(Guid MemberId, Guid MediaItemId);
