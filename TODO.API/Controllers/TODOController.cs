using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TODO.API.Data.Interfaces;
using TODO.API.DTOs;
using TODO.API.Models;

namespace TODO.API.Controllers
{
    [ApiController]
    [Route("api/todo")]


    public class ToDoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;
        public ToDoController(ITodoRepository todoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAllItems()
        {

            var items = _todoRepository.GetAllData();

            var itemsMapped = _mapper.Map<GetItemDataDto>(items);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(itemsMapped);
        }

        [HttpGet("{ItemId}")]
        public IActionResult GetDataById(int ItemId)
        {
            if (!_todoRepository.ItemExist(ItemId)) { return NotFound(); }

            var retrievedItem = _mapper.Map<GetItemDataDto>(_todoRepository.GetItemById(ItemId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(retrievedItem);
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromBody] CreateItemDataDto createItemData)
        {
            if (createItemData == null) return BadRequest(ModelState);


            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var crateItemMap = _mapper.Map<ItemData>(createItemData);

            if (!_todoRepository.CreateItem(crateItemMap))
            {
                ModelState.AddModelError("", "failed to map data");
                return StatusCode(500, ModelState);

            }
            return Ok();
        }

        [HttpPut("{ItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateData(int ItemId, [FromBody] UpdateItemDataDto updateItemData)
        {
            if (updateItemData is null)
                return BadRequest(ModelState);

            if (ItemId != updateItemData.Id)
                return BadRequest(ModelState);

            if (!_todoRepository.ItemExist(ItemId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var itemMap = _mapper.Map<ItemData>(updateItemData);

            if (!_todoRepository.UpdateItem(itemMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Item");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{ItemId}")]
        public IActionResult DeleteCategory(int ItemId)
        {
            if (!_todoRepository.ItemExist(ItemId))
            {
                return NotFound();
            }

            var itemToDelete = _todoRepository.GetItemById(ItemId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_todoRepository.DeleteItem(itemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Item");
            }

            return NoContent();
        }

    }
}
