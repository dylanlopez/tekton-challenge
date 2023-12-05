using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.WebApi.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[ProducesResponseType(typeof(Response<ProductByIdQueryResponse>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(Response<ProductByIdQueryResponse>), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> GetAll()
		{
			var request = new ProductAllQueryRequest()
			{
				StatusId = 1
			};
			var result = await _mediator.Send(request);
			if (result.State == 400)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpGet("{idProduct}")]
		[ProducesResponseType(typeof(Response<ProductByIdQueryResponse>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(Response<ProductByIdQueryResponse>), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> GetActiveBreakOfTheDay([FromRoute] int idProduct)
		{
			var request = new ProductByIdQueryRequest()
			{
				ProductId = idProduct
			};
			var result = await _mediator.Send(request);
			if (result.State == 400)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost("create")]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.Created)]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] ProductCreateCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.State == 400)
			{
				return BadRequest(result);
			}
			return Created("create", result);
		}

		[HttpDelete("delete")]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Delete([FromBody] ProductDeleteCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.State == 400)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPut("update")]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Update([FromBody] ProductUpdateCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.State == 400)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPatch("update-stock")]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdateStock([FromBody] ProductUpdateStockCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.State == 400)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}
	}
}
