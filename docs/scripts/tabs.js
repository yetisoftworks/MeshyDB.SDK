    function openCode(codeName, elmnt, color) {
      // Hide all elements with class="tabcontent" by default */
      var i, tabcontent, tablinks;
      tabcontent = document.getElementsByClassName("tabcontent");
      for (i = 0; i & lt; tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
      }

      // Remove the background color of all tablinks/buttons
      tablinks = document.getElementsByClassName("tablink");
      for (i = 0; i & lt; tablinks.length; i++) {
        tablinks[i].style.backgroundColor = "";
        tablinks[i].style.color = "black";
      }

      // Show the specific tabs content
      var elements = document.getElementsByClassName(codeName);
      for (i = 0; i & lt; elements.length; i++) {
        elements[i].style.display = "block";
      }

      // Add the specific color to the button used to open the tab content
      elements = document.getElementsByClassName(elmnt.className);
      for (i = 0; i & lt; elements.length; i++) {
        elements[i].style.backgroundColor = color;
        elements[i].style.color = "white";
      }
      
      // Get the element with id="defaultOpen" and click on it
      if (document.getElementsByClassName("objbutton") !== null) {
        var elements = document.getElementsByClassName("objbutton");
        for (i = 0; i & lt; elements.length; i++) {
          elements[i].click();
        }
      }
    }
