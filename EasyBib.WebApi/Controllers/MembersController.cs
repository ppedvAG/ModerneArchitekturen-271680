using EasyBib.Domain.Contracts;
using EasyBib.Domain.Entities;
using EasyBib.Domain.Enums;
using EasyBib.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyBib.WebApi;

// ============================================================
// MembersController (CRUD + Abo-Wechsel)
// ============================================================
[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    public MembersController(IMemberRepository memberRepository)
        => _memberRepository = memberRepository;

    [HttpGet("{id}")]
    public ActionResult<MemberDto> GetById(Guid id)
    {
        var member = _memberRepository.GetById(id);
        if (member is null) return NotFound();
        return Ok(new MemberDto(member.Id, member.Name, member.Email));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMemberRequest request)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Membership = request.Plan is null ? null : new Membership
            {
                Id = Guid.NewGuid(),
                PlanName = request.Plan.Value,
                MaxActiveLoans = request.Plan.Value switch
                {
                    MembershipPlan.Basic => 2,
                    MembershipPlan.Premium => 10,
                    MembershipPlan.Family => 5,
                    _ => 0
                },
                LoanPeriodDays = 28
            }
        };

        _memberRepository.Add(member);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, new MemberDto(member.Id, member.Name, member.Email));
    }

    [HttpPut("{id}/plan")]
    public IActionResult ChangePlan(Guid id, [FromBody] MembershipPlan plan)
    {
        var member = _memberRepository.GetById(id);
        if (member is null) return NotFound();

        member.Membership ??= new Membership { Id = Guid.NewGuid(), MemberId = member.Id };
        member.Membership.PlanName = plan;
        _memberRepository.Update(member);
        return NoContent();
    }
}
