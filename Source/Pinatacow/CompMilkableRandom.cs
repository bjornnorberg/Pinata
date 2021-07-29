﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Pinatacow
{
    public class CompMilkableRandom : CompHasGatherableBodyResource
    {
        protected override int GatherResourcesIntervalDays
        {
            get
            {
                return this.Props.IntervalDays;
            }
        }

        protected override int ResourceAmount
        {
            get
            {
                var rng = new Random();
                return rng.Next(this.Props.minAmount, this.Props.maxAmount + 1);
            }
        }

        // This is what returns what object is being gathered.
        protected override ThingDef ResourceDef
        {
            get
            {
                return this.Props.defList.RandomElement();
            }
        }

        protected override string SaveKey
        {
            get
            {
                return "Bowab_pinataFullness";
            }
        }

        public CompProperties_MilkableRandom Props
        {
            get
            {
                return (CompProperties_MilkableRandom)this.props;
            }
        }

        protected override bool Active
        {
            get
            {
                if (!base.Active)
                {
                    return false;
                }
                Pawn pawn = this.parent as Pawn;
                return (!this.Props.FemaleOnly || pawn == null || pawn.gender == Gender.Female) && (pawn == null || pawn.ageTracker.CurLifeStage.milkable);
            }
        }

        public override string CompInspectStringExtra()
        {
            if (!this.Active)
            {
                return null;
            }
            return "Bowab_PinataFullness".Translate() + ": " + base.Fullness.ToStringPercent();
        }
    }
}