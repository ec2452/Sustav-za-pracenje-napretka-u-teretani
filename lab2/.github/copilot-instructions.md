# Copilot Workspace Instructions

## Mandatory UX Delegation For UI Code
- When the user asks for UI, frontend, component, layout, styling, page, view, or accessibility work, first delegate to the `ux-agent` sub-agent.
- The main agent must request structure, component states, responsive behavior, and accessibility decisions from `ux-agent` before writing final UI code.
- If the task is mixed (backend + frontend), delegate only the UI part to `ux-agent`, then continue implementation.
- Do not skip delegation for UI generation unless the user explicitly asks to bypass UX review.
- The delegation must be traceable: the workspace hook writes a terse `UX_SUBAGENT ux-agent` record to `logs/agent_log.txt` when the sub-agent starts.

## Trigger Keywords
Treat requests containing these words as UI tasks and delegate to `ux-agent`:
- ui, ux, frontend, front-end, component, layout, page, view, responsive, css, styling, design system, accessibility, a11y, form, dashboard, navigation

## Required Output Integration
After delegation, the main agent should incorporate:
- Visual hierarchy rationale
- Component set with states (default, hover, focus, active, disabled, error, loading, empty when applicable)
- Responsive behavior at core breakpoints
- Accessibility notes (focus, contrast, labels, keyboard)
