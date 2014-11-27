$(document).ready(function () {
    
        $(".bracket").hide();
   
});

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
    var players = $('#participantList p');
    var rows = 2 * Math.pow(2, Math.ceil(Math.log(players.length) / Math.log(2)));
    var cols = 1 + (3 * Math.ceil((Math.log(players.length) / Math.log(2))));
    var strTemp;
    alert("Nb. of players: " + players.length + " Rows:" + rows + " Cols:" + cols);
    if (adminName != '' && tournamentName != '' && date != '' && players.length != 0) {
        $.ajax({  
            url: '../Tournament/Brackets',
            type: 'POST',
            data: { adminName: adminName, tournamentName: tournamentName, date: date, type:type, seed: seed },
            success: function (data) {
                $("#participantsDiv").slideUp();
                $("#bracketWinners").slideDown();
                if (type == "Double Elimination") {
                    $("#bracketLosers").slideDown();
                }
                
                for (i = 1; i <= cols; i++) {
                    $('#bracketWinners').append('<ul id="col' + i + '">' + '</ul>');
                    strTemp = "#bracketWinners ul#col" + i;
                    //alert(strTemp)
                    //$(strTemp).append('<li id="col' + i + '>' + i + '-' + i + '</li>');
                    //$('#bracketWinners').append('<p>' + rows + '</p>');
                    for (j = 1; j <= (rows); j++) {
                        $(strTemp).append('<li>' + i + ' ' + j + '</li>');
                        //$(strTemp).append('<li id="col' + j + '>' + i + '-' + j + '</li>');
                    }
                }

                
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

                $('#participantList').append('<p>' + participantName + ';' + participantSponsor + ';' + participantTeam + ';' + participantSeed + '</p>');
            },
            error: function (xhr, ajaxOptions, thrownError) {
        }

        });
    }
};