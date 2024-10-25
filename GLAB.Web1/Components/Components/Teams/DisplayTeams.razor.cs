using System.Data.SqlTypes;
using GLAB.App.Laboratories;
using GLAB.App.Teams;
using GLAB.Domains.Models.Teams;
using GLAB.Implementation.Services.Teams;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace GLAB.Web1.Components.Components.Teams;

partial class DisplayTeams
{
    private List<Team> teams;
    private List<Team> searchedTeams;
    private string searchedTeamName;
    private ElementReference searchBar;
    private Timer searchTimer;
    private bool confirmDialogShown;
    private Team targetTeam;
    private bool detailedCardVisible;

    [Inject] private ITeamService teamService { get; set; }
    [Inject] private IJSRuntime js { get; set; }
    
    [Inject] private AuthenticationStateProvider auth { get; set; }

    
    protected override async Task OnInitializedAsync()
    {
       var authState = await auth.GetAuthenticationStateAsync();
  
       if (authState != null)
       {
         var labClaim=  authState.User.Claims.FirstOrDefault(claim => claim.Type.Equals("LabId"));
         var labId=  labClaim != null ? labClaim.Value:null;
         
         if (labId != null)
         {
             teams = await teamService.GetTeamsByLaboratory(labId);
             searchedTeams = teams;
         }
         
       }
    
    }
    
    private  void  TimerCallback(Object o)
    {
         search();

    }

    private async Task search()
    {
        
        searchedTeams = teams.FindAll(team => team.TeamName.Contains(searchedTeamName));
       await InvokeAsync(() =>
        {
            StateHasChanged();
        });
    }

    

    private async Task searchTeam()
    {
        
        if (String.IsNullOrEmpty(searchedTeamName))
        {
            searchedTeams = teams;
            return;
        }
        
        if (searchTimer != null)
        {
           await searchTimer.DisposeAsync();
          
        }
        
        searchTimer = new Timer(TimerCallback, null, 0, 300);

    }

 

    private async Task deleteTeam(Team team)
    {
        targetTeam = team;
        confirmDialogShown = true;
    }

    private async Task deleteTeamConfirmation(Object response)
    {
        try
        {
            if(response is Team team)
            {
              
                
            }
            else if (response is bool)
            {
                await js.InvokeVoidAsync("hideConfirmDialog");
                await Task.Delay(300);
                confirmDialogShown = false;
            }
            
        }
        catch (Exception e)
        {
            throw e;
        }
        
    }

    private async Task showMoreDetails(Team team)
    {
        targetTeam = team;
        detailedCardVisible = true;
        StateHasChanged();
        await Task.Delay(5);
        await js.InvokeVoidAsync("displayDetailedTeamCard",team.TeamId);
        
    }

    private async Task closeDetailedCardView()
    {
        detailedCardVisible = false;
    }
}