//The play/pause icon.
define(function(){
	'use strict';
	var playPauseButton = $('#PlayPauseButton'), pauseIcon = $('#pauseIcon'), playIcon = $('#playIcon');
    
	function refresh() {
	    var player = chrome.extension.getBackgroundPage().YoutubePlayer;
	    
	    //Change the music button to the 'Play' image and cause a song to play upon click.
        function setToPlay() {
            pauseIcon.hide();
            playIcon.show();

            playPauseButton.off('click').on('click', function () {
                chrome.extension.getBackgroundPage().YoutubePlayer.play();
                pauseIcon.show();
                playIcon.hide();
            });
        }
	    
	    //Change the music button to the 'Pause' image and cause a song to pause upon click.
        function setToPause() {
            pauseIcon.show();
            playIcon.hide();

            playPauseButton.off('click').on('click', function () {
                chrome.extension.getBackgroundPage().YoutubePlayer.pause();
                pauseIcon.hide();
                playIcon.show();
            });
        }
	    
        if (player.playerState === PlayerStates.PLAYING) {
            setToPause();
        }
        else if (!player.playerIsSeeking) {
            setToPlay();
        }
	    
        if (player.selectedItem) {
            //Paint playPauseButton's path black and allow it to be clicked.
            playPauseButton.removeClass('disabled');
            playPauseButton.find('.path').css('fill', 'black');
        } else {
            //Disable the button such that it cannot be clicked.
            //NOTE: Pause button will never be displayed disabled.
            setToPlay();
            playPauseButton.addClass('disabled').off('click');
            playPauseButton.find('.path').css('fill', 'gray');
        }
	}
    
    //Initialize
    refresh();
    
	return {
	    refresh: refresh
	};
});