
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
                if (participantSponsor == "")
                    participantSponsor = "no sponsor"
                if (participantTeam == "")
                    participantTeam = "no team"
                if (participantSeed == "")
                    participantSeed = "0"

                $('#participantsDiv').append('<p>' + participantName + ';' + participantSponsor + ';' + participantTeam + ';' + participantSeed + '</p>' );
                //$('#participants').val($('#participants').val() + participantName);
            },
            error: function (xhr, ajaxOptions, thrownError) {
        }

        });
    }
};