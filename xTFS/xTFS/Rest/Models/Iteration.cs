﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class Iteration
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public IterationAttributes Attributes { get; set; }
	}
}
