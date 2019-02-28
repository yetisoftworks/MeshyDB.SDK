$(':input.code-tab').click(function(){$(':input.code-tab[data-lang="'+ $(this).attr('data-lang')+'"]:not(:checked)').not(this).click() })
