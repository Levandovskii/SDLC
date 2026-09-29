#import "template.typ": *

#include "document/front-page-sdlc.typ"

#show: template

#include "document/report.typ"

#attachment(
  "обязательное",
  "Листинг кода"
)

#source-text(
  "../README.md",
  "Файл README",
)

#source-text(
  "../.gitignore",
  "ProjectTemplate .gitignore",
)