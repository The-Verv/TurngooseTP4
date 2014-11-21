
var addParticipant = function () {
    var participantName = $('#txtPartName').val().trim();
    var participantSponsor = $('#txtPartSponsors').val().trim();
    var participantTeam = $('#txtPartTeam').val().trim();
    var participantSeed = $('#txtPartSeed').val().trim();
    
    if (participantName != '') {
        console.log(participantName);
        //$('#participants').val($('#participants').val() + participantName);
        $.ajax({
           
            url: '../Tournament/Index',
            type: 'POST',
            data: { name: participantName, sponsor: participantSponsor, team: participantTeam, seed: participantSeed },
            success: function (participantName) {
                $('#participants').val($('#participants').val() + participantName);
                alert("ajax");
               
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert("errorfgt");
        }

        });
    }
};