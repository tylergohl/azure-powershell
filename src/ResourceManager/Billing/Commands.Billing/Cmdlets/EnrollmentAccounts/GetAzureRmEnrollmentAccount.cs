﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Billing.Common;
using Microsoft.Azure.Commands.Billing.Models;
using Microsoft.Azure.Management.Billing;
using Microsoft.Azure.Management.Billing.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Billing.Cmdlets.BillingPeriods
{
    [Cmdlet(VerbsCommon.Get, "AzureRmBillingPeriod", DefaultParameterSetName = Constants.ParameterSetNames.ListParameterSet), OutputType(typeof(List<PSBillingPeriod>), typeof(PSBillingPeriod))]
    public class GetAzureRmEnrollmentAccount : AzureBillingCmdletBase
    {
        [Parameter(Mandatory = true, HelpMessage = "Name of the enrollment account to get.", ParameterSetName = Constants.ParameterSetNames.SingleItemParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ParameterSetName.Equals(Constants.ParameterSetNames.SingleItemParameterSet))
            {
                WriteObject(new PSEnrollmentAccount(BillingManagementClient.EnrollmentAccounts.Get(Name)));
            }
            else
            {
                try
                {
                    WriteObject(BillingManagementClient.BillingPeriods.List().Select(x => new PSEnrollmentAccount(x)), enumerateCollection: true);
                }
                catch (ErrorResponseException error)
                {
                    WriteWarning(error.Body.Error.Message);
                }
                return;
            }
        }
    }
}
