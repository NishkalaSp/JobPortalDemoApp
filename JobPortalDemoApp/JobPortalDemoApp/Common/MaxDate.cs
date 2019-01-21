using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalDemoApp.Common
{
    public class MaxDate : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var dob = Convert.ToDateTime(value).Date;

            if (dob < DateTime.Now.Date)
            {
                return true;
            }
            return false;
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            
            rule.ValidationType = "maxdate";
            rule.ValidationParameters.Add("currentdate", DateTime.Now.Date);
            yield return rule;
        }
    }
}