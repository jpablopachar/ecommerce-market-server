---
applyTo: "**"
---
# Commit Message Generation Instructions

## Format

- Use conventional format: `type(scope): description`
- Types: feat, fix, docs, style, refactor, test, chore
- Optional scope in parentheses (entity, dto, controller, etc.)
- Concise description in imperative mood, no period at end
- You must keep the names if they are technical themes or programming code

## Content

- First line should not exceed 72 characters
- Describe WHAT changed and WHY, not HOW
- Use Spanish for the description
- Maintain consistency with previous commits

## Type Definitions

- `feat`: A new feature
- `fix`: A bug fix
- `docs`: Documentation only changes
- `style`: Changes that do not affect the meaning of the code (white-space, formatting, etc.)
- `refactor`: A code change that neither fixes a bug nor adds a feature
- `test`: Adding missing tests or correcting existing tests
- `chore`: Changes to the build process or auxiliary tools

## Common Scopes

- `api`: Changes related to API endpoints or controllers
- `auth`: Authentication and authorization features
- `entity`: Entity model changes
- `dto`: Data Transfer Object modifications
- `db`: Database related changes (migrations, context, etc.)
- `config`: Configuration and setup changes
- `ui`: User interface components (if applicable)
- `service`: Service layer implementations
- `repo`: Repository layer implementations
- `test`: Test infrastructure (separate from the 'test' type)
- `docs`: Documentation specific scopes
- `deps`: Dependency management

## Examples

- `feat(auth): implementar autenticación basada en JWT`
- `fix(entity): corregir relación entre Product y Category`
- `docs(api): añadir documentación para endpoints de productos`
- `refactor(service): mejorar el rendimiento del servicio de catálogo`
- `chore(deps): actualizar paquetes Microsoft.EntityFrameworkCore`
- `test(api): añadir pruebas para ProductController`
- `feat(db): añadir migraciones para nuevas entidades de pedidos`
- `style(dto): aplicar convenciones de nomenclatura a DTOs`
