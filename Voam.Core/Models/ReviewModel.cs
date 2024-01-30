using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public double Rating { get; set; }
    }
}
