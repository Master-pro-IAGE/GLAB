using GLAB.App.Memebers;
using GLAB.Domains.Models.Members;
using Microsoft.AspNetCore.Components;

namespace GLAB.Web1.Components.Components.Members;

partial class DisplayMembersCards
{
    [Inject] private IMemberService memberService { get; set; }
    public List<Member> members { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        members = await memberService.GetMembers();
        
    }
    
}