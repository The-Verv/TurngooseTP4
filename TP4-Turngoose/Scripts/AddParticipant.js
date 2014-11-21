
var addParticipant = function () {
    var participantName = $('#txtPartName').val().trim();
    var participantSponsor = $('#txtPartSponsors').val().trim();
    var participantTeam = $('#txtPartTeam').val().trim();
    var participantSeed = $('#txtPartSeed').val().trim();
    
    if (participantName != '') {
        $.ajax({  
            url: '../Tournament/AddParticipant',
            type: 'POST',
            data: { name: participantName, sponsor: participantSponsor, team: participantTeam, seed: participantSeed },
            success: function (data) {
                $('#participants').val($('#participants').val() + participantName);
                alert("ajax");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert("errorfgt");
        }

        });
    }
};