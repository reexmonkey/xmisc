using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.concretes.infrastructure
{
    public class Fpi: FpiBase
    {
        public Fpi(ApprovalStatus status, string author, string product, string description, string language, string reference = null) 
            : base(status, author, product, description, language, reference)
        {
        }

        public Fpi(string value)
        {
            const RegexOptions options = RegexOptions.IgnoreCase
                                         | RegexOptions.CultureInvariant
                                         | RegexOptions.ExplicitCapture
                                         | RegexOptions.Compiled;

            const string pattern = @"^(?<prefix>)//(?<product>)//(?<desc>)//(?<lang>)*$";
            foreach (Match match in Regex.Matches(value, pattern, options))
            {
                if (match.Groups["prefix"].Success)
                {
                    switch (match.Groups["prefix"].Value)
                    {
                        case "+": Status = ApprovalStatus.Informal; break;

                        case "-": Status = ApprovalStatus.None; break;

                        default:
                            Status = ApprovalStatus.Standard;
                            Reference = match.Groups["prefix"].Value;
                            break;
                    }
                }
                if (match.Groups["author"].Success) Author = match.Groups["author"].Value;
                if (match.Groups["product"].Success) Product = match.Groups["product"].Value;
                if (match.Groups["desc"].Success && !string.IsNullOrWhiteSpace(match.Groups["desc"].Value))
                    Description = match.Groups["desc"].Value.TrimStart();
                if (match.Groups["lang"].Success) Language = match.Groups["lang"].Value;
            }


        }

        public override void FromUrn(string urn)
        {
            throw new NotImplementedException();
        }
    }
}
