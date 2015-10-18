﻿using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.BackOffice.Tax
{
    public partial class CountySalesTaxes : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "county_sales_tax_id";
                scrud.TableSchema = "core";
                scrud.Table = "county_sales_taxes";
                scrud.ViewSchema = "core";
                scrud.View = "county_sales_tax_scrud_view";
                scrud.Text = Titles.CountySalesTaxes;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.counties.county_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "CountyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.entities.entity_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "EntityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.industries.industry_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "IndustryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.item_groups.item_group_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "ItemGroupDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.counties.county_id", "core.county_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.entities.entity_id", "core.entity_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.industries.industry_id", "core.industry_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.item_groups.item_group_id", "core.item_group_scrud_view");
            return string.Join(",", displayViews);
        }
    }
}