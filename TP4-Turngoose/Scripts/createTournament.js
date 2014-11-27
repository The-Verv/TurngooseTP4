var randSeed = function () {
    if ($('#randSeed').prop('checked')) {
        $('#txtPartSeed').val(" ");
        $('#txtPartSeed').attr('disabled', true);
    } else {
        $('#txtPartSeed').attr('disabled', false);
    }
};

var createTournament = function () {
    var adminName = $('#txtAdmin').val().trim();
    var tournamentName = $('#txtName').val().trim();
    var date = $('#txtDate').val().trim();
    var type = $('#tournamentType').val();
    var seed = $('#randSeed').prop('checked');
    if (adminName != '' && tournamentName != '' && date != '' && type != '' && seed != '') {
        $.ajax({  
            url: '../Tournament/Brackets',
            type: 'POST',
            data: { adminName: adminName, tournamentName: tournamentName, date: date, type:type, seed: seed },
            success: function (data) {
                //alert("Worked")
            },
            error: function (xhr, ajaxOptions, thrownError) {
               alert("Un ou des champs obligatoires sont manquants.")
            }

        });
    }
};



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
            },
            error: function (xhr, ajaxOptions, thrownError) {
        }

        });
    }
};