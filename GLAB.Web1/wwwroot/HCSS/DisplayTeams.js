
async function displayDetailedTeamCard(card){
    var teamCardBaseInfo=document.querySelector(".teamCardBaseInfo");
    card.classList.add("rotated");
    
    await new Promise(resolve => setTimeout(resolve,300));
        
    card.classList.remove("teamCardContainerBaseInfo");
}

async function HideDetailedTeamCard(card){
    var teamCardBaseInfo=document.querySelector(".teamCardBaseInfo");
    card.classList.remove("rotated");
    await new Promise(resolve => setTimeout(resolve,300));
    card.classList.add("teamCardContainerBaseInfo");

}