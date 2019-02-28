if(localStorage["selectedLang"]){
  $(':input.code-tab[data-lang="'+ localStorage["selectedLang"]  +'"]:not(:checked)').click();
}

$(':input.code-tab').click(function(){
  const selectedLang = $(this).attr('data-lang');
  $(':input.code-tab[data-lang="'+ selectedLang  +'"]:not(:checked)').not(this).click();
  localStorage["selectedLang"] = selectedLang;
});
