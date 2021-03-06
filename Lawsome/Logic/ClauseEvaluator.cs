﻿using Lawesome.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawesome.Logic
{
    abstract class ClauseEvaluator
    {
        public string ID { get; set; }

        public abstract EvaluationResult Evaluate(Contract contract, Clause clause, int whoAmI);
    }
}
