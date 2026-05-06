(function () {
    const selectors = {
        focusable: ".tv-focusable",
        scope: "[data-focus-scope]",
        overlay: "[data-overlay]"
    };

    let lastFocusedElement = null;

    function isVisible(element) {
        if (!element || element.hidden) {
            return false;
        }
        try {
            const style = window.getComputedStyle(element);
            if (style.display === 'none' || style.visibility === 'hidden' || parseFloat(style.opacity) === 0) {
                return false;
            }
        }
        catch (e) {
            // ignore and continue
        }
        return element.getClientRects && element.getClientRects().length > 0;
    }

    function getActiveOverlay() {
        return Array.from(document.querySelectorAll(selectors.overlay)).find(isVisible) || null;
    }

    function getSidebarToggle() {
        return document.querySelector("[data-sidebar-toggle]");
    }

    function isSidebarCollapsed() {
        return document.body.classList.contains("sidebar-collapsed");
    }

    function toggleSidebar(forceExpanded) {
        const shouldExpand = typeof forceExpanded === "boolean" ? forceExpanded : isSidebarCollapsed();
        document.body.classList.toggle("sidebar-collapsed", !shouldExpand);
        const toggle = getSidebarToggle();
        if (toggle) {
            toggle.setAttribute("aria-expanded", shouldExpand ? "true" : "false");
        }
        return shouldExpand;
    }

    function getFocusableElements(scope) {
        return Array.from(scope.querySelectorAll(selectors.focusable)).filter((element) => isVisible(element) && !element.hasAttribute("disabled"));
    }

    function getFocusablePool() {
        const overlay = getActiveOverlay();
        return overlay ? getFocusableElements(overlay) : Array.from(document.querySelectorAll(selectors.focusable)).filter((element) => isVisible(element) && !element.hasAttribute("disabled"));
    }

    function getActiveScope() {
        const overlay = getActiveOverlay();
        if (overlay) {
            const overlayScopes = Array.from(overlay.querySelectorAll(selectors.scope)).filter(isVisible);
            return overlayScopes[0] || overlay;
        }
        return Array.from(document.querySelectorAll(selectors.scope)).filter(isVisible)[0] || document.body;
    }

    function getSectionForElement(element) {
        const scopedSection = element.closest(selectors.scope);
        const overlay = element.closest(selectors.overlay);
        if (scopedSection && overlay && overlay.contains(scopedSection)) {
            return scopedSection;
        }
        return scopedSection || overlay || document.body;
    }

    function setFocused(element) {
        if (!element || !isVisible(element)) {
            return;
        }
        document.querySelectorAll(".is-focused").forEach((node) => node.classList.remove("is-focused"));
        element.classList.add("is-focused");
        element.focus({ preventScroll: true });
        element.scrollIntoView({ block: "nearest", inline: "nearest" });
        lastFocusedElement = element;
    }

    function getSectionEntry(section) {
        if (!section) {
            return null;
        }
        const focusables = getFocusableElements(section);
        if (focusables.length === 0) {
            return null;
        }
        if (section.id === "sideNavSection") {
            return section.querySelector(".nav-item.active") || focusables.find((element) => element.hasAttribute("data-autofocus")) || focusables[0];
        }
        return focusables.find((element) => element.hasAttribute("data-autofocus")) || focusables[0];
    }

    function focusInitial(scope) {
        const target = getSectionEntry(scope);
        if (target) {
            setFocused(target);
        }
    }

    function getRectMetrics(element) {
        const rect = element.getBoundingClientRect();
        return { left: rect.left, top: rect.top, centerX: rect.left + (rect.width / 2), centerY: rect.top + (rect.height / 2) };
    }

    function buildRows(elements) {
        const tolerance = 24;
        const rows = [];
        elements.forEach((element) => {
            const metrics = getRectMetrics(element);
            let row = rows.find((candidate) => Math.abs(candidate.top - metrics.top) <= tolerance);
            if (!row) {
                row = { top: metrics.top, items: [] };
                rows.push(row);
            }
            row.items.push({ element: element, left: metrics.left, centerX: metrics.centerX });
        });
        rows.sort((a, b) => a.top - b.top);
        rows.forEach((row) => row.items.sort((a, b) => a.left - b.left));
        return rows;
    }

    function getRowPosition(rows, element) {
        for (let rowIndex = 0; rowIndex < rows.length; rowIndex += 1) {
            const itemIndex = rows[rowIndex].items.findIndex((item) => item.element === element);
            if (itemIndex !== -1) {
                return { rowIndex: rowIndex, itemIndex: itemIndex };
            }
        }
        return null;
    }

    function findClosestInRow(row, centerX) {
        if (!row || row.items.length === 0) {
            return null;
        }
        let best = row.items[0];
        let bestDistance = Math.abs(best.centerX - centerX);
        row.items.forEach((item) => {
            const distance = Math.abs(item.centerX - centerX);
            if (distance < bestDistance) {
                bestDistance = distance;
                best = item;
            }
        });
        return best.element;
    }

    function resolveTarget(target, currentElement) {
        if (!target) {
            return null;
        }
        if (target === "self") {
            return currentElement;
        }
        const node = document.querySelector(target);
        if (!node) {
            return null;
        }
        return node.matches(selectors.focusable) ? node : getSectionEntry(node);
    }

    function getExplicitTarget(element, direction) {
        return resolveTarget(element.dataset[`nav${direction.charAt(0).toUpperCase()}${direction.slice(1)}`], element);
    }

    function getSectionTarget(section, direction, currentElement) {
        return resolveTarget(section.dataset[`nav${direction.charAt(0).toUpperCase()}${direction.slice(1)}`], currentElement);
    }

    function getSectionType(section) {
        return section.dataset.navType || "free";
    }

    function getSidebarSelectedItem() {
        const sidebar = document.getElementById("sideNavSection");
        return sidebar ? sidebar.querySelector(".nav-item.active") || sidebar.querySelector(".nav-item") : null;
    }

    function getSidebarFallback(section, direction) {
        if (direction !== "left" || document.body.classList.contains("player-mode")) {
            return null;
        }
        const sidebar = document.getElementById("sideNavSection");
        if (!sidebar || section === sidebar || sidebar.contains(section)) {
            return null;
        }
        return getSidebarSelectedItem();
    }

    function getSidebarContentTarget(current) {
        const sidebar = document.getElementById("sideNavSection");
        if (!sidebar || !sidebar.contains(current)) {
            return null;
        }
        const selectedItem = getSidebarSelectedItem();
        return getExplicitTarget(selectedItem || current, "right") || getSectionTarget(sidebar, "right", selectedItem || current);
    }

    function getPrimaryItems(section) {
        const focusables = getFocusableElements(section);
        const primary = focusables.filter((element) => element.dataset.navRole !== "subaction");
        return primary.length > 0 ? primary : focusables;
    }

    function findGeometryOutsideSection(current, direction, section) {
        const pool = getFocusablePool().filter((element) => !section.contains(element));
        if (pool.length === 0) {
            return current;
        }
        const currentMetrics = getRectMetrics(current);
        let best = null;
        let bestScore = Number.POSITIVE_INFINITY;
        pool.forEach((candidate) => {
            const candidateMetrics = getRectMetrics(candidate);
            const dx = candidateMetrics.centerX - currentMetrics.centerX;
            const dy = candidateMetrics.centerY - currentMetrics.centerY;
            if (direction === "left" && dx >= -4) { return; }
            if (direction === "right" && dx <= 4) { return; }
            if (direction === "up" && dy >= -4) { return; }
            if (direction === "down" && dy <= 4) { return; }
            const primary = direction === "left" || direction === "right" ? Math.abs(dx) : Math.abs(dy);
            const secondary = direction === "left" || direction === "right" ? Math.abs(dy) : Math.abs(dx);
            const score = primary + (secondary * 2.2);
            if (score < bestScore) {
                bestScore = score;
                best = candidate;
            }
        });
        return best || current;
    }

    function moveInVerticalList(section, current, direction) {
        const items = getFocusableElements(section);
        const index = items.indexOf(current);
        if (index === -1) {
            return current;
        }
        if (direction === "up") {
            return items[index - 1] || getSectionTarget(section, "up", current) || current;
        }
        if (direction === "down") {
            return items[index + 1] || getSectionTarget(section, "down", current) || current;
        }
        if (section.id === "sideNavSection" && direction === "right") {
            return getSidebarContentTarget(current) || current;
        }
        return getSectionTarget(section, direction, current) || getSidebarFallback(section, direction) || current;
    }

    function moveInHorizontal(section, current, direction) {
        const items = getFocusableElements(section);
        const index = items.indexOf(current);
        if (index === -1) {
            return current;
        }
        if (direction === "left") {
            return items[index - 1] || getSectionTarget(section, "left", current) || getSidebarFallback(section, "left") || current;
        }
        if (direction === "right") {
            return items[index + 1] || getSectionTarget(section, "right", current) || current;
        }
        return getSectionTarget(section, direction, current) || getSidebarFallback(section, direction) || findGeometryOutsideSection(current, direction, section) || current;
    }

    function moveInGrid(section, current, direction, options) {
        const settings = options || {};
        if (current.dataset.navRole === "subaction") {
            const parent = resolveTarget(current.dataset.parentCard, current);
            if (direction === "up" || direction === "down" || direction === "left") {
                return parent || current;
            }
            if (direction === "right") {
                return parent ? moveInGrid(section, parent, "right", { skipSubaction: true }) : current;
            }
            return parent || current;
        }

        const items = getPrimaryItems(section);
        const rows = buildRows(items);
        const position = getRowPosition(rows, current);
        if (!position) {
            return current;
        }
        const row = rows[position.rowIndex];
        const currentItem = row.items[position.itemIndex];

        if (direction === "right" && !settings.skipSubaction) {
            const subaction = current.querySelector('[data-nav-role="subaction"]');
            if (subaction && isVisible(subaction) && !subaction.hasAttribute("disabled")) {
                return subaction;
            }
        }
        if (direction === "left") {
            return row.items[position.itemIndex - 1]?.element || getSectionTarget(section, "left", current) || getSidebarFallback(section, "left") || current;
        }
        if (direction === "right") {
            return row.items[position.itemIndex + 1]?.element || getSectionTarget(section, "right", current) || current;
        }
        if (direction === "up") {
            const prevRow = rows[position.rowIndex - 1];
            return (prevRow ? findClosestInRow(prevRow, currentItem.centerX) : null) || getSectionTarget(section, "up", current) || current;
        }
        if (direction === "down") {
            const nextRow = rows[position.rowIndex + 1];
            return (nextRow ? findClosestInRow(nextRow, currentItem.centerX) : null) || getSectionTarget(section, "down", current) || current;
        }
        return current;
    }

    function findNextFocusable(current, direction) {
        const explicitTarget = getExplicitTarget(current, direction);
        if (explicitTarget) {
            return explicitTarget;
        }
        const section = getSectionForElement(current);
        const type = getSectionType(section);
        if (type === "vertical-menu" || type === "vertical-list") {
            return moveInVerticalList(section, current, direction);
        }
        if (type === "horizontal-actions" || type === "horizontal-rail" || type === "choice-row") {
            return moveInHorizontal(section, current, direction);
        }
        if (type === "single-item" || type === "single-input") {
            return getSectionTarget(section, direction, current) || getSidebarFallback(section, direction) || current;
        }
        if (type === "grid") {
            return moveInGrid(section, current, direction);
        }
        return findGeometryOutsideSection(current, direction, section);
    }

    function showToast(message, title) {
        const stack = document.getElementById("toastStack");
        if (!stack) {
            return;
        }
        const toast = document.createElement("article");
        toast.className = "toast";
        toast.innerHTML = `<div class="toast-title">${title || "Demo Action"}</div><div class="toast-copy">${message}</div>`;
        stack.appendChild(toast);
        window.setTimeout(() => toast.remove(), 2800);
    }

    function navigateToUrl(url) {
        if (url) {
            window.location.href = url;
        }
    }

    function closeOverlay(overlay) {
        if (!overlay) {
            return;
        }
        overlay.hidden = true;
        const restoreId = overlay.dataset.restoreFocusId;
        if (restoreId) {
            const restoreElement = document.getElementById(restoreId);
            if (restoreElement) {
                setFocused(restoreElement);
                return;
            }
        }
        focusInitial(getActiveScope());
    }

    function openOverlay(overlay, source) {
        if (!overlay) {
            return;
        }
        overlay.hidden = false;
        if (source && source.id) {
            overlay.dataset.restoreFocusId = source.id;
        }
        focusInitial(overlay);
    }

    function toggleFavorite(button) {
        const active = button.dataset.favoriteState === "true";
        button.dataset.favoriteState = (!active).toString();
        button.classList.toggle("is-active", !active);
        showToast(active ? "Removed from favorites" : "Added to favorites");
    }

    function toggleSwitch(button) {
        const nextState = !button.classList.contains("is-on");
        button.classList.toggle("is-on", nextState);
        showToast(nextState ? "Setting enabled for the demo" : "Setting disabled for the demo");
    }

    function selectChoice(button) {
        const group = button.closest("[data-choice-group]");
        if (!group) {
            return;
        }
        group.querySelectorAll(".setting-choice").forEach((item) => item.classList.remove("is-selected"));
        button.classList.add("is-selected");
        showToast(`${button.dataset.choiceLabel || "Option"} selected`);
    }

    function activateElement(element) {
        if (!element) {
            return;
        }
        if (element.hasAttribute("data-sidebar-toggle")) {
            const expanded = toggleSidebar();
            showToast(expanded ? "Menu expanded" : "Menu collapsed", "Navigation");
            setFocused(element);
            return;
        }
        if (element.hasAttribute("data-open-overlay")) {
            openOverlay(document.querySelector(element.getAttribute("data-open-overlay")), element);
            return;
        }
        if (element.hasAttribute("data-close-overlay")) {
            closeOverlay(element.closest(selectors.overlay));
            return;
        }
        if (element.hasAttribute("data-favorite-toggle")) {
            toggleFavorite(element);
            return;
        }
        if (element.classList.contains("settings-toggle")) {
            toggleSwitch(element);
            return;
        }
        if (element.classList.contains("setting-choice")) {
            selectChoice(element);
            return;
        }
        if (element.dataset.playerChannel) {
            navigateToUrl(`/player/${element.dataset.playerChannel}`);
            return;
        }
        if (element.dataset.href) {
            navigateToUrl(element.dataset.href);
            return;
        }
        if (element.dataset.toast) {
            showToast(element.dataset.toast);
            return;
        }
        if (element.getAttribute("href")) {
            navigateToUrl(element.getAttribute("href"));
        }
    }

    function handleBackAction() {
        const overlay = getActiveOverlay();
        if (overlay) {
            closeOverlay(overlay);
            return;
        }
        if (!isSidebarCollapsed() && !document.body.classList.contains("player-mode")) {
            toggleSidebar(false);
            const toggle = getSidebarToggle();
            if (toggle) {
                setFocused(toggle);
            }
            return;
        }
        const backUrl = document.body.dataset.backUrl;
        if (window.location.pathname !== "/" && backUrl) {
            navigateToUrl(backUrl);
        }
    }

    function normalizeFavorites() {
        document.querySelectorAll("[data-favorite-toggle]").forEach((button) => button.classList.toggle("is-active", button.dataset.favoriteState === "true"));
    }

    document.addEventListener('DOMContentLoaded', function () {
        const row = document.querySelector('.player-schedule-row');
        if (!row) return; // safety check

        let scrollInterval;

        row.addEventListener('mousemove', (e) => {
            const rect = row.getBoundingClientRect();
            const x = e.clientX - rect.left;

            const threshold = rect.width * 0.95;

            if (x > threshold) {
                clearInterval(scrollInterval);
                scrollInterval = setInterval(() => {
                    row.scrollLeft += 10;
                }, 5);
            } else {
                clearInterval(scrollInterval);
            }
        });

        row.addEventListener('mouseleave', () => {
            clearInterval(scrollInterval);

            row.scrollTo({
                left: 0,
                behavior: 'smooth'
            });
        });
    });

    document.addEventListener("keydown", (event) => {
        const activeElement = document.activeElement && document.activeElement.matches(selectors.focusable) ? document.activeElement : (lastFocusedElement || getFocusablePool()[0]);
        if (!activeElement) {
            return;
        }
        const directionalMap = { ArrowLeft: "left", ArrowRight: "right", ArrowUp: "up", ArrowDown: "down" };
        if (directionalMap[event.key]) {
            event.preventDefault();
            setFocused(findNextFocusable(activeElement, directionalMap[event.key]));
            return;
        }
        if (event.key === "Enter") {
            event.preventDefault();
            activateElement(activeElement);
            return;
        }
        if (event.key === "Escape" || event.key === "Backspace") {
            event.preventDefault();
            handleBackAction();
        }
    });

    document.addEventListener("focusin", (event) => {
        const target = event.target;
        if (target instanceof HTMLElement && target.matches(selectors.focusable)) {
            setFocused(target);
        }
    });

    document.addEventListener("click", (event) => {
        const target = event.target instanceof Element ? event.target.closest(selectors.focusable) : null;
        if (!target) {
            return;
        }
        event.preventDefault();
        activateElement(target);
    });

    window.tvApp = {
        ...(window.tvApp || {}),
        showToast: showToast,
        toggleSidebar: toggleSidebar,
        ensureSignalRConnected: window.tvSignalR?.ensureConnected
    };

    normalizeFavorites();
    const toggle = getSidebarToggle();
    if (toggle) {
        toggle.setAttribute("aria-expanded", isSidebarCollapsed() ? "false" : "true");
    }

    const shouldSkipInitialFocus = document.body.dataset.skipInitialFocus === "true";

    if (!shouldSkipInitialFocus) {
        focusInitial(getActiveScope());
    }
})();
