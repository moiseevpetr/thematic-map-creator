﻿using System;
using System.IO;
using System.Threading.Tasks;
using BAMCIS.GeoJSON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThematicMapCreator.Api.Services;

namespace ThematicMapCreator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly FilesService service;

        public FilesController(FilesService service)
        {
            this.service = service;
        }

        [HttpPost("csv")]
        public async Task<ActionResult<GeoJson>> GetGeoJsonFromCsv(IFormFile file)
        {
            if (".csv" != Path.GetExtension(file.FileName).ToLower())
            {
                return BadRequest("Расширение файла должно быть \"CSV\".");
            }

            try
            {
                await using var stream = file.OpenReadStream();
                using var streamReader = new StreamReader(stream);

                var csv = await streamReader.ReadToEndAsync();
                var geoJson = service.ConvertCsvToGeoJson(csv);

                return Ok(geoJson);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}