if(localStorage["selectedLang"]){
  $(':input.code-tab[data-lang="'+ localStorage["selectedLang"]  +'"]:not(:checked)').click();
}
$(':input.code-tab').click(function(e){
	if(e.hasOwnProperty('originalEvent')){
	  const selectedLang = $(this).attr('data-lang');
	  $(':input.code-tab[data-lang="'+ selectedLang  +'"]:not(:checked)').click();
	  localStorage["selectedLang"] = selectedLang;
  }
});
