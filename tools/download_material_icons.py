#!/usr/bin/env python3
"""
Download Google Material Icons (Outlined) from GitHub and replace OLW toolbar icons.
Icons are resized to 16x16, tinted stone-900 (#1c1917) on transparent background.
"""

import os
import urllib.request
import io
from PIL import Image

REPO = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
BASE = "https://raw.githubusercontent.com/google/material-design-icons/master/png"

ICON_COLOR    = (28, 25, 23)   # stone-900 - normal
ICON_SELECTED = (180, 83, 9)   # amber-700 - selected/pushed
ICON_DISABLED = (188, 175, 160) # muted warm - disabled

def url(category, name):
    return f"{BASE}/{category}/{name}/materialicons/24dp/1x/baseline_{name}_black_24dp.png"

def download(category, name):
    try:
        req = urllib.request.Request(url(category, name), headers={"User-Agent": "Mozilla/5.0"})
        with urllib.request.urlopen(req, timeout=12) as r:
            return r.read()
    except Exception as e:
        print(f"  WARN: {category}/{name}: {e}")
        return None

def tint(data, color, alpha_mul=1.0):
    img = Image.open(io.BytesIO(data)).convert("RGBA").resize((16, 16), Image.LANCZOS)
    r, g, b = color
    px = img.load()
    for y in range(16):
        for x in range(16):
            _, _, _, a = px[x, y]
            if a > 0:
                px[x, y] = (r, g, b, min(255, int(a * alpha_mul)))
    return img

def save(img, path):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    img.save(path, "PNG")

# ── paths ─────────────────────────────────────────────────────────────────
P = {
    "AF":   os.path.join(REPO, "src/managed/OpenLiveWriter.ApplicationFramework/Commands/Images"),
    "PE":   os.path.join(REPO, "src/managed/OpenLiveWriter.PostEditor/Commands/Images"),
    "HE":   os.path.join(REPO, "src/managed/OpenLiveWriter.HtmlEditor/Commands/Images"),
    "HL":   os.path.join(REPO, "src/managed/OpenLiveWriter.HtmlEditor/Linking/Commands/Images"),
    "TB":   os.path.join(REPO, "src/managed/OpenLiveWriter.PostEditor/Tables/Commands/Images"),
    "PH":   os.path.join(REPO, "src/managed/OpenLiveWriter.PostEditor/PostHtmlEditing/Commands/Images"),
}

# (command_name, category, material_name, group_key)
ICONS = [
    # ── ApplicationFramework ──────────────────────────────────────────────
    ("CommandInsertLink",        "content",    "link",                    "AF"),
    ("CommandPaste",             "content",    "content_paste",           "AF"),
    ("CommandSave",              "content",    "save",                    "AF"),
    ("CommandUndo",              "content",    "undo",                    "AF"),
    ("CommandBlockquote",        "editor",     "format_quote",            "AF"),
    ("CommandUnderline",         "editor",     "format_underlined",       "AF"),
    ("CommandMenu",              "navigation", "menu",                    "AF"),
    ("CommandFont",              "editor",     "format_size",             "AF"),
    ("CommandPrint",             "action",     "print",                   "AF"),
    ("CommandHelp",              "action",     "help",                    "AF"),
    ("CommandCheckSpelling",     "action",     "spellcheck",              "AF"),
    ("CommandCut",               "content",    "content_cut",             "AF"),
    ("CommandFontColor",         "editor",     "format_color_text",       "AF"),
    ("CommandIndent",            "editor",     "format_indent_increase",  "AF"),
    ("CommandItalic",            "editor",     "format_italic",           "AF"),
    ("CommandColorize",          "image",      "palette",                 "AF"),
    ("CommandCopy",              "content",    "content_copy",            "AF"),
    ("CommandClear",             "action",     "delete",                  "AF"),
    ("CommandNumbers",           "editor",     "format_list_numbered",    "AF"),
    ("CommandBold",              "editor",     "format_bold",             "AF"),
    ("CommandRedo",              "content",    "redo",                    "AF"),
    ("CommandBullets",           "editor",     "format_list_bulleted",    "AF"),
    ("CommandOutdent",           "editor",     "format_indent_decrease",  "AF"),
    ("CommandStrikethrough",     "editor",     "format_strikethrough",    "AF"),

    # ── PostEditor ───────────────────────────────────────────────────────
    ("CommandOpenPost",          "file",       "folder_open",             "PE"),
    ("CommandConfigureWeblog",   "action",     "settings",                "PE"),
    ("CommandWeblogMenu",        "social",     "public",                  "PE"),
    ("CommandPostAndPublish",    "file",       "cloud_upload",            "PE"),
    ("CommandPostProperties",    "image",      "tune",                    "PE"),
    ("CommandToolsMenu",         "action",     "build",                   "PE"),
    ("CommandInsertMenu",        "content",    "add_circle",              "PE"),
    ("CommandNewPost",           "editor",     "edit_note",               "PE"),
    ("CommandAddWeblog",         "content",    "add_circle",              "PE"),
    ("CommandViewWeblog",        "action",     "open_in_new",             "PE"),
    ("CommandSavePost",          "content",    "save",                    "PE"),

    # ── HtmlEditor ──────────────────────────────────────────────────────
    ("CommandSpellCheck",        "action",     "spellcheck",              "HE"),

    # ── HtmlEditor Linking ───────────────────────────────────────────────
    ("CommandGlossary",          "action",     "subject",                 "HL"),

    # ── Tables ───────────────────────────────────────────────────────────
    ("CommandInsertColumnRight", "action",     "view_week",               "TB"),
    ("CommandMoveColumnRight",   "hardware",   "keyboard_tab",            "TB"),
    ("CommandMoveRowUp",         "hardware",   "keyboard_arrow_up",       "TB"),
    ("CommandInsertRowBelow",    "action",     "view_list",               "TB"),
    ("CommandTableProperties",   "image",      "grid_on",                 "TB"),
    ("CommandClearCell",         "action",     "delete",                  "TB"),
    ("CommandInsertTable",       "action",     "table_view",              "TB"),
    ("CommandInsertColumnLeft",  "action",     "view_week",               "TB"),
    ("CommandInsertTable2",      "action",     "table_view",              "TB"),
    ("CommandTableMenu",         "action",     "table_view",              "TB"),
    ("CommandInsertRowAbove",    "action",     "view_list",               "TB"),
    ("CommandDeleteTable",       "action",     "delete_forever",          "TB"),
    ("CommandMoveRowDown",       "hardware",   "keyboard_arrow_down",     "TB"),
    ("CommandMoveColumnLeft",    "hardware",   "keyboard_tab",            "TB"),

    # ── PostHtmlEditing ──────────────────────────────────────────────────
    ("CommandViewNormal",               "action",     "wysiwyg",         "PH"),
    ("CommandRemoveLink",               "content",    "link_off",        "PH"),
    ("CommandInsertExtendedEntry",      "navigation", "more_horiz",      "PH"),
    ("CommandClipboardMenu",            "content",    "content_paste",   "PH"),
    ("CommandInsertPicture",            "image",      "image",           "PH"),
    ("CommandInsertLinkToOnfolioItem",  "action",     "bookmark",        "PH"),
    ("CommandViewCode",                 "action",     "code",            "PH"),
    ("CommandImageSaveDefaults",        "content",    "save",            "PH"),
    ("CommandImageDecoratorAddMenu",    "image",      "auto_fix_high",   "PH"),
    ("CommandImageReset",               "navigation", "refresh",         "PH"),
    ("CommandImageDecoratorRemove",     "maps",       "layers_clear",    "PH"),
    ("CommandImageBrightness",          "image",      "brightness_6",    "PH"),
    ("CommandImageRotate",              "image",      "rotate_right",    "PH"),
]

# commands that need a Selected state file
NEEDS_SELECTED = {"CommandSpellCheck"}

def run():
    cache = {}
    ok = fail = 0

    for cmd, cat, mat, grp in ICONS:
        key = (cat, mat)
        if key not in cache:
            print(f"  {cat}/{mat}", end=" ... ", flush=True)
            cache[key] = download(cat, mat)
            print("ok" if cache[key] else "FAIL")

        data = cache[key]
        if data is None:
            fail += 1
            continue

        out = P[grp]
        enabled  = os.path.join(out, f"{cmd}CommandBarButtonBitmapEnabled.png")
        selected = os.path.join(out, f"{cmd}CommandBarButtonBitmapSelected.png")
        disabled = os.path.join(out, f"{cmd}CommandBarButtonBitmapDisabled.png")

        save(tint(data, ICON_COLOR), enabled)

        if cmd in NEEDS_SELECTED or os.path.exists(selected):
            save(tint(data, ICON_SELECTED), selected)

        if cmd in NEEDS_SELECTED or os.path.exists(disabled):
            save(tint(data, ICON_DISABLED, 0.5), disabled)

        ok += 1

    print(f"\nDone: {ok} icons replaced, {fail} failed")

if __name__ == "__main__":
    run()
