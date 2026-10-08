using EasyBib.Domain.Contracts;
using EasyBib.Domain.Entities;
using EasyBib.Domain.Services;
using EasyBib.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyBib.WebApi;

// ============================================================
// LoansController (prozessgetrieben, KEIN CRUD)
// ============================================================
[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly LoanService _loanService;
    private readonly ILoanRepository _loanRepository;

    public LoansController(LoanService loanService, ILoanRepository loanRepository)
    {
        _loanService = loanService;
        _loanRepository = loanRepository;
    }

    [HttpPost("checkout")]
    public ActionResult<LoanDto> CheckOut([FromBody] CheckOutRequest request)
    {
        try
        {
            var loan = _loanService.CheckOut(request.MemberId, request.MediaItemId);
            return Ok(ToDto(loan));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public ActionResult<LoanDto> GetById(Guid id)
    {
        var loan = _loanRepository.GetById(id);
        if (loan is null) return NotFound();
        return Ok(ToDto(loan));
    }

    [HttpPost("{id}/return")]
    public IActionResult Return(Guid id)
    {
        var loan = _loanRepository.GetById(id);
        if (loan is null) return NotFound();
        loan.MarkReturned();
        _loanRepository.Save(loan);
        return NoContent();
    }

    [HttpPost("{id}/extend")]
    public IActionResult Extend(Guid id, [FromQuery] int days = 7)
    {
        var loan = _loanRepository.GetById(id);
        if (loan is null) return NotFound();
        try
        {
            loan.Extend(days);
            _loanRepository.Save(loan);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    private LoanDto? ToDto(Loan loan)
    {
        return loan is null ? null : new LoanDto(loan.Id, loan.MembershipId, loan.MediaItemId, loan.Status, loan.DueDate);
    }
}