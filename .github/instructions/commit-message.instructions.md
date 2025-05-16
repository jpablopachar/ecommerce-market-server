---
applyTo: "**"
---
# Commit Message Generation Instructions

## Format
All commit messages should follow this format:

## Content
- Write all commit messages in Spanish
- Use imperative, present tense (e.g., "añade" not "añadido" or "añadiendo")
- First line should be 50 characters or less
- Descriptions should be clear and descriptive
- Reference issues and pull requests when relevant using #<issue-number>

## Type Definitions
- **feat**: A new feature
- **fix**: A bug fix
- **docs**: Documentation only changes
- **style**: Changes that do not affect the meaning of the code (formatting, etc)
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **perf**: A code change that improves performance
- **test**: Adding missing or correcting existing tests
- **chore**: Changes to the build process or auxiliary tools

## Common Scopes
- **api**: Changes to the API controllers or endpoints
- **model**: Changes to data models
- **config**: Configuration changes
- **auth**: Authentication related changes
- **ui**: User interface changes
- **db**: Database related changes
- **weather**: Weather forecast functionality

## Examples
- `feat(api): añade endpoint para búsqueda por fecha`
- `fix(weather): corrige cálculo de temperatura en Fahrenheit`
- `docs(readme): actualiza instrucciones de instalación`
- `refactor(model): simplifica modelo de pronóstico del tiempo`
- `test(api): añade pruebas para el controlador WeatherForecast`
- `chore(deps): actualiza paquetes NuGet`