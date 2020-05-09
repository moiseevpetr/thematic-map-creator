﻿using System;
using BAMCIS.GeoJSON;
using ThematicMapCreator.Api.Models;

namespace ThematicMapCreator.Api.Contracts
{
    public class LayerOverview
    {
        public GeoJson Data { get; set; }
        public Guid Id { get; set; }
        public int Index { get; set; }
        public Guid MapId { get; set; }
        public string Name { get; set; }
        public LayerStyle Style { get; set; }
        public LayerType Type { get; set; }
        public bool Visible { get; set; }
    }
}
