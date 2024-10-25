using GLAB.Domains.Models.Members;
using Microsoft.AspNetCore.Components;

namespace GLAB.Web1.Components.Components.Members;

partial class MemberCard
{
    [Parameter] public Member member { get; set; }
}