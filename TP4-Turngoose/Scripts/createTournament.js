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
    var nameCount = 0;
    var rowMod4 = 0;
    var rowModBatl = 0;
    var colMod3 = 0
    var battleMatrix = [[4, 3], [4, 11], [4, 19], [4, 27]];

    //alert("Nb. of players: " + players.length + " Rows:" + rows + " Cols:" + cols);
    if (adminName != '' && tournamentName != '' && date != '' && players.length != 0) {
        $.ajax({  
            url: '../Tournament/Brackets',
            type: 'POST',
            data: { adminName: adminName, tournamentName: tournamentName, date: date, type:type, seed: seed },
            success: function (data) {
                $("#participantsDiv").slideUp();
                $("#bracketWinners").slideDown();
                $("#undoDiv").slideDown();
                if (type == "Double Elimination") {
                    $("#bracketLosers").slideDown();
                }
                //boucle générant le tableau selon le nombre de joueurs
                //          créer un modulo permettant de savoir à quelles rangées il y a un match selon le nombre de joueurs
                for (i = 1; i <= cols; i++) {
                    colMod3 = (i - 1) % 3; //est égal à 0 sur les colonnes 1,4,7,10,13...
                    //alert("colmod: " + colMod3)
                    //$('#bracketWinners').append('<ul id="col' + i + '">' + '</ul>');
                    strTemp = "#bracketWinners ul#col" + i;
                    for (j = 0; j < rows ; j++) {
                        if (i == 1) {
                            rowMod4 = j % 4;
                            if (rowMod4 <= 2 && rowMod4 > 0) {
                                //$(strTemp).append('<li class="player">' + players.get(nameCount).innerHTML + '</li>');
                                nameCount++;
                            }
                            else if (rowMod4 == 3 || rowMod4 == 0) {
                                //$(strTemp).append('<li class="blank">/</li>');
                            }
                        } else if (colMod3 == 0) {
                            //alert("col: " + i)
                        } else {
                            //$(strTemp).append('<li>' + i + '-' + j + '</li>');
                        }
                    }
                }

                
            },
            error: function (xhr, ajaxOptions, thrownError) {
               alert("Un ou des champs obligatoires sont manquants.")
            }

        });
    }
};
var createTournament8 = function () {
    var adminName = $('#txtAdmin').val().trim();
    var tournamentName = $('#txtName').val().trim();
    var date = $('#txtDate').val().trim();
    var type = $('#tournamentType').val();
    var seed = $('#randSeed').prop('checked');
    var players = $('#participantList p');

    if (adminName != '' && tournamentName != '' && date != '' && players.length != 0) {
        $.ajax({
            url: '../Tournament/Brackets',
            type: 'POST',
            data: { adminName: adminName, tournamentName: tournamentName, date: date, type: type, seed: seed },
            success: function (data) {
                $("#participantsDiv").slideUp();
                $("#bracketWinners8").slideDown();
                $("#undoDiv").slideDown();
                if (type == "Double Elimination") {
                    $("#bracketLosers8").slideDown();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert("Un ou des champs obligatoires sont manquants.")
            }

        });
    }
};

var addLoser = function (ddl, bracketID) {
    if (ddl.selectedIndex != 0) {
        var loser;
        if (ddl.selectedIndex == 1)
            loser = ddl.options[2].value;
        else
            loser = ddl.options[1].value;

        ddl.options[0].value = document.getElementById(bracketID).innerHTML;
        document.getElementById(bracketID).innerHTML = loser;
        var list = document.getElementById(bracketID),
        items = list.childNodes;

        for (var i = 0, length = childNodes.length; i < length; i++) {
            if (items[i].nodeType != 1) {
                continue;
            }
            items[i].innerHTML = loser;
        }
    }
    
}

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