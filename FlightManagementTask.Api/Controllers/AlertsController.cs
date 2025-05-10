using FlightManagementTask.Application.IService;
using FlightManagementTask.Common.Extensions;
using FlightManagementTask.DAL.Entities;
using FlightManagementTask.Model.Dto;
using FlightManagementTask.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagementTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _service;

        public AlertsController(IAlertService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ApiResponse<List<PriceAlertDto>>>> GetUserAlerts(string userId)
        {
            var alerts = await _service.GetUserAlertsAsync(userId);

            var result = alerts.Select(a => new PriceAlertDto
            {
                Id = a.Id,
                UserId = a.UserId,
                Origin = a.Origin,
                Destination = a.Destination,
                DepartureDate = a.DepartureDate,
                TargetPrice = a.TargetPrice,
                Currency = a.Currency,
                CreatedAt = a.CreatedAt,
                IsActive = a.IsActive
            }).ToList();

            return Ok(ApiResponse<List<PriceAlertDto>>.Success(result));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PriceAlertDto>>> CreateAlert([FromBody] PriceAlertCreateDto dto)
        {
            try
            {
                var ilsPrice = dto.TargetPrice.ToILS(dto.Currency);

                var alert = new PriceAlert
                {
                    UserId = dto.UserId,
                    Origin = dto.Origin,
                    Destination = dto.Destination,
                    DepartureDate = dto.DepartureDate,
                    TargetPrice = ilsPrice,
                    Currency = "ILS",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _service.AddAsync(alert);

                var result = new PriceAlertDto
                {
                    Id = alert.Id,
                    UserId = alert.UserId,
                    Origin = alert.Origin,
                    Destination = alert.Destination,
                    DepartureDate = alert.DepartureDate,
                    TargetPrice = alert.TargetPrice,
                    Currency = alert.Currency,
                    CreatedAt = alert.CreatedAt,
                    IsActive = alert.IsActive
                };

                return CreatedAtAction(nameof(GetUserAlerts), new { userId = dto.UserId }, ApiResponse<PriceAlertDto>.Success(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PriceAlertDto>.Fail(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PriceAlertDto>>> UpdateAlert(Guid id, [FromBody] PriceAlertCreateDto dto)
        {
            var existing = await _service.GetAlertById(id);
            if (existing == null)
                return NotFound(ApiResponse<PriceAlertDto>.Fail("Alert not found"));

            try
            {
                existing.Origin = dto.Origin;
                existing.Destination = dto.Destination;
                existing.DepartureDate = dto.DepartureDate;
                existing.TargetPrice = dto.TargetPrice.ToILS(dto.Currency);
                existing.Currency = "ILS";
                existing.IsActive = true;

                await _service.UpdateAsync(existing);

                var updated = new PriceAlertDto
                {
                    Id = existing.Id,
                    UserId = existing.UserId,
                    Origin = existing.Origin,
                    Destination = existing.Destination,
                    DepartureDate = existing.DepartureDate,
                    TargetPrice = existing.TargetPrice,
                    Currency = existing.Currency,
                    CreatedAt = existing.CreatedAt,
                    IsActive = existing.IsActive
                };

                return Ok(ApiResponse<PriceAlertDto>.Success(updated));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PriceAlertDto>.Fail(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAlert(Guid id)
        {
            var existing = await _service.GetAlertById(id);
            if (existing == null)
                return NotFound(ApiResponse<string>.Fail("Alert not found"));

            await _service.DeleteAsync(id);
            return Ok(ApiResponse<string>.Success("Alert deleted successfully"));
        }
    }
}
