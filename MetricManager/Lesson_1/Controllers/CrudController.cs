using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson_1.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private ValuesHolder _holder;
        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }
        [HttpPost("create")]
        public IActionResult Create ([FromQuery] string key, [FromQuery] string value)
        {
            _holder.Add(value);
            return Ok();
        }
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string stringsToUpdate, [FromQuery] string newValue)
        {
            for(var i = 0; i < _holder.Values.Count(); i++)
            {
                if (_holder.Values[i] == stringsToUpdate)
                    _holder.Values[i] = newValue;
            }
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            _holder.Values = _holder.Values.Where(w => w != stringsToDelete).ToList();
            return Ok();
        }
    }
}

namespace Lesson_1
{
    public class ValuesHolder
    {
        private List<string> _values;
        public List<string> Values
        { 
            get => _values; 
            set => _values = value; 
        }
        public void Add(string input)
        {
            _values.Add(input);
        }
        public List<string> Get()
        {
            return _values;
        }

    }
}