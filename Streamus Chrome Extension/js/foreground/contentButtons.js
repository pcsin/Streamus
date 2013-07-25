define(['settingsManager'], function(settingsManager){
    'use strict';

    console.log("Loaded");

    var menuButtons = $('.menubutton');
    
    //  User clicks on a different button on the LHS, possible change of content display.
    menuButtons.click(function () {

        console.log("Clicked");

        //  If the user clicked a button that isn't the current button.
        if (!$(this).hasClass('active')) {
            //  Clear content and show new content based on button clicked.
            menuButtons.removeClass('active');
            
            $(this).addClass('active');
            $('.content:visible').hide();
            $('#' + $(this).data('content')).show();

            settingsManager.set('activeContentButtonId', this.id);
        }
    });

    //  Set the initially loaded content to whatever was clicked last or the home page as a default
    var activeContentButtonId = settingsManager.get('activeContentButtonId');
    $('#' + activeContentButtonId).click();
});