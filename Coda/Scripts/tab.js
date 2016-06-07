﻿var is_tab_text_changed = false; var max_line_character_count = 110; var popover_show = null; var $save_btn = null; var $wiki_form = null; var $edit_text = null; var $chords_find = null; var $marker = null; var revision_key = tab_id + "-rev-wiki-tab"; var form_fields_to_save = null; $(function () { popover_show = false; $save_btn = $("#js-save-btn"); $edit_text = $("#js-tab-text"); $wiki_form = $("#edit-wiki-tab-form"); $chords_find = $("#js-chords-finder"); $marker = $("#js-mark-chords"); form_fields_to_save = [$(".js-artist"), $(".js-song"), $("#js-tab-type"), $(".js-tab-part"), $(".js-tab-tuning")]; $(".js-wiki-save-form").sisyphus({ customKeySuffix: tab_id, excludeFields: $("#comment, #termSearch, #termReplace, #caseSensitive, .js-approve-comment, #js-chords-finder"), onSave: function () { setToStorage(revision_key, current_revision_id); showDraftToolTip("All changes saved to draft", 7000) }, onRelease: function () { removeFromStorage(revision_key) }, onBeforeRestore: function () { var a = getFromStorage(revision_key); if (a != null) { if (a != current_revision_id) { $.ajax({ url: "/contribution/wiki/getDiff?id=" + tab_id, data: { revision_id: current_revision_id, text: getFromStorage(getLocalStorageTabTextKey()) }, type: "POST", dataType: "json", success: function (b) { if (b.result == "ok") { $(".js-modal-label").text("Tab versions conflict"); $(".js-modal-body").html('<div class="alert alert-danger">\n                                        While you were away, someone had updated this tab from your previous unsaved version <b>' + a + "</b> to the most recent version <b>" + current_revision_id + '</b>.<br>\n                                        Would you like to <a href="" id="js-remove-draft">delete</a> or <a href="" id="js-restore-draft">restore</a> \n                                        your unsaved version? The changes are shown below. \n                                    </div>' + b.info); $("#modal").modal("show") } } }) } else { restoreTabFromDraft() } } return false }, }); $(document).on("click", "#js-remove-draft", function () { clearDraft(); $("#modal").modal("toggle"); return false }); $(document).on("click", "#js-restore-draft", function () { restoreTabFromDraft(); setToStorage(revision_key, current_revision_id); $("#modal").modal("toggle"); return false }); $edit_text.keypress(function () { if (!is_tab_text_changed) { showWikiButtons(); is_tab_text_changed = true } }); $(document).on("beforeunload", function () { if (is_tab_text_changed) { return "You have unsaved changes." } }); $wiki_form.submit(function () { if ($edit_text.length > 0) { $edit_text.val(replaceTabSymbolsToSpaces($edit_text.val(), "auto")) } }); $("#js-discuss-btn").click(function (a) { $.ajax({ url: "/contribution/wiki/createComment", type: "GET", data: { id: tab_id, "WikiTabComment[parent_id]": 0, "WikiTabComment[wiki_tab_id]": tab_id, "WikiTabComment[text]": $(".js-discuss-text").val() }, dataType: "json", success: function (b) { if (b.result == "ok") { $(".js-comment-item").remove(); $(".js-comment-wrapper").prepend(b.info); $(".js-discuss-text").val("") } } }); return false }); $(".js-author-popover-target").hover(function () { var a = $(this); $.ajax({ url: "/user/info/contributor", data: { user_id: a.attr("data-user"), level: a.attr("data-level"), }, dataType: "jsonp", success: function (b) { if (a.is(":hover")) { a.popover({ html: true, placement: "top", content: b.info, container: a }).popover("show") } } }) }, function () { $(this).popover("hide").popover("destroy") }); $(".js-view-diff").click(function (a) { revision_id = $(this).attr("data-revision"); $.ajax({ url: "/contribution/wiki/getDiff?id=" + tab_id, data: { revision_id: revision_id, text: $edit_text.val() }, type: "POST", dataType: "json", success: function (b) { if (b.result == "ok") { $(".js-modalDiff-label").text("Compare your tab with v." + revision_id); $(".js-modalDiff-body").html(b.info); $("#modalDiff").modal("show") } } }); return false }); $(".js-restore-text").click(function (a) { if (confirm("Do you really wanna rollback all your changes and restore original tab text?")) { revision_id = $(this).attr("data-revision"); $.ajax({ url: "/contribution/wiki/getRevision", data: { rev: revision_id, id: tab_id }, type: "GET", dataType: "json", success: function (b) { if (b.result == "ok") { $edit_text.val(b.info); clearDraft() } } }) } return false }); $(".js-search-bnt").click(function (a) { $(".js-search-panel").toggle(); $(".js-find-text").focus(); return false }); $(".js-search-close").click(function (a) { $(".js-search-panel").hide() }); $(".js-find").click(function () { sr_widget.find(); return false }); $(".js-replace").click(function () { sr_widget.findAndReplace(); return false }); $(".js-replace-all").click(function () { sr_widget.replaceAll(); return false }); $("#termSearch").pressEnter(function () { sr_widget.find() }); $("#termReplace").pressEnter(function () { sr_widget.findAndReplace() }); $("#js-add-tuning").on("click", function () { $.post("/contribution/submit/add-tuning", { tab_text: $edit_text.val(), tuning: $(".js-tab-tuning").val(), tab_type: $("#js-tab-type").val() }, function (a) { $edit_text.val(a) }); return false }); $(".js-tooltip").tooltip(); if (tab_has_chords) { $chords_find.bootstrapSwitch({ state: $chords_find.attr("checked"), onColor: "success", offColor: "danger", }); if (is_manually_highlighted) { unsetAutoFindChords(true) } else { setAutoFindChords() } } $chords_find.on("switchChange.bootstrapSwitch", function (b, a) { if (a) { unsetAutoFindChords(false) } else { setAutoFindChords() } }); $edit_text.keydown(function (f) { if (f.ctrlKey && f.keyCode == 49 && ($chords_find.bootstrapSwitch("state"))) { var f = document.getElementById($edit_text.attr("id")); var c = document.selection; f.focus(); if (!c) { var b = f.selectionStart; var d = f.selectionEnd; var a = f.value.substring(b, d); $.ajax({ url: "/contribution/submit/markChords", data: { tab_text: a }, type: "POST", success: function (e) { if (e) { $edit_text.val(f.value.substring(0, b) + e + f.value.substring(d)) } } }) } return false } }); $(document).on("click", ".js-brother", function () { $.ajax({ url: "/contribution/submit/getBrother?id=" + tab_id, data: { brother_id: $(this).attr("data-brother"), brother_action: $(this).attr("data-brother-action"), }, type: "GET", dataType: "json", success: function (a) { if (a.result == "ok") { $(".js-modal-label").text("Compare this tab with another version"); $(".js-modal-body").html(a.info); $("#modal").modal("show") } } }); return false }); $(".js-approve-tab").click(function () { if ($(this).attr("data-confirm") == 1) { $("#modalApprove").modal("show") } else { $("#modalApprove").modal("hide"); var a = false; if (check_max_width) { a = hasLongLines() } if (a) { tourApproval.start(true) } else { approveMakeDecision(1, "") } } }); $("#js-reason-reject-check").change(function () { $(this).is(":checked") ? $("#js-reason-reject-other").show() : $("#js-reason-reject-other").hide() }); $("#js-reason-reject-accuracy").change(function () { $(this).is(":checked") ? $("#js-reason-reject-accuracy-container").show() : $("#js-reason-reject-accuracy-container").hide() }); $(".js-btn-reject").click(function () { var a = true; var d = true; var c = 0; var b = ""; $(".js-reject-reason").each(function () { if ($(this).is(":checked")) { c++; if ($(this).attr("data-reason")) { b = b + $(this).attr("data-reason") } } }); if ($("#js-reason-reject-check").is(":checked") && ($(".js-reason-reject-input").val().length < 3) && (c == 1)) { d = false } if ($("#js-reason-reject-accuracy").is(":checked") && ($(".js-reason-reject-accuracy-input").val().length < 10)) { a = false } if ($(".js-reason-reject-accuracy-input").val()) { b += " " + $(".js-reason-reject-accuracy-input").val() } if ($(".js-reason-reject-input").val()) { b += " " + $(".js-reason-reject-input").val() } !c ? $(".js-reject-reason-count").addClass("has-error") : $(".js-reject-reason-count").removeClass("has-error"); if (!d) { $(".js-reject-other-error").show(); $("#js-reason-reject-other").addClass("has-error") } else { $(".js-reject-other-error").hide(); $("#js-reason-reject-other").removeClass("has-error") } if (!a) { $(".js-reject-accuracy-error").show(); $("#js-reason-reject-accuracy-container").addClass("has-error") } else { $(".js-reject-accuracy-error").hide(); $("#js-reason-reject-accuracy-container").removeClass("has-error") } if (c && d && a) { $("#modalReject").modal("hide"); approveMakeDecision(0, b) } return false }); $("#js-approve-comment-btn").click(function (b) { var a = $(".js-approve-comment").val(); if (($(".js-approve-vote-editor").text()) != "") { $("#js-approve-comment-btn").attr("disabled", true); $.ajax({ url: "/contribution/submit/createApproveComment", type: "GET", data: { id: tab_id, text: a }, dataType: "json", success: function (c) { if (c.result == "ok") { $(".js-comment-item").remove(); $(".js-comment-wrapper").prepend(c.info); $(".js-approve-comment").val(""); $(".js-approve-vote-editor").html(""); $("#js-approve-comment-btn").attr("disabled", false) } } }) } return false }); $("#js-tab-text").on({ keyup: detectTabLongLines }); $("#js-tab-text").on({ keyup: detectUserInteraction }); updateTabLimiter(); $("#js-submit-long-line-manual").click(function (a) { $("#longLineModal").modal("hide"); return false }); $("#js-submit-long-line-auto").click(function (a) { correctTabText(); return false }); $("#js-correct-tab").click(function (a) { correctTabText(function () { }); return false }) }); var tab_limiter_length = 110; function updateTabLimiter() { var a = $('<span class="form-control tab-font"></span>'); var c = ""; var d = 0; for (var b = 0; b < tab_limiter_length; b++) { c += "t" } a.text(c).css({ display: "inline-block", width: "auto" }).appendTo("body"); d = Math.floor(((a.width() * (tab_limiter_length + 1) / tab_limiter_length) + a.outerWidth()) / 2); $(".js-tab-limiter").css({ left: d }); a.remove() } function getTabLongLines() { var a = $("#js-tab-text").val().replace(/\[ch\]|\[\/ch\]/ig, "").split("\n"); var c = []; for (var b = 0; b < a.length; ++b) { if (a[b].length > tab_limiter_length) { c[b] = a[b] } } return c } function hasTabLongLines() { return getTabLongLines().length > 0 } function detectTabLongLines() { $(".js-tab-area")[hasTabLongLines() ? "addClass" : "removeClass"]("has_long_lines") } var user_interaction_callback = null; function setUserInteractionEvent(a) { user_interaction_callback = a } function onUserInteractionTimer() { if (user_interaction_callback) { user_interaction_callback() } } var user_interaction_timer = null; function detectUserInteraction() { if (user_interaction_timer) { clearTimeout(user_interaction_timer) } user_interaction_timer = setTimeout(onUserInteractionTimer, 3000) } function showDraftToolTip(a, b) { if (!popover_show) { popover_show = true; $edit_text.popover({ content: a, placement: "left", container: "body" }).popover("show"); setTimeout("$edit_text.popover('destroy'); popover_show = false;", b) } } function setToStorage(a, b) { if (typeof $.jStorage === "object") { $.jStorage.set(a, b + "") } else { try { localStorage.setItem(a, b + "") } catch (c) { } } } function getFromStorage(b) { if (typeof $.jStorage === "object") { var a = $.jStorage.get(b); return a ? a.toString() : a } else { return localStorage.getItem(b) } } function removeFromStorage(a) { if (typeof $.jStorage === "object") { $.jStorage.deleteKey(a) } else { localStorage.removeItem(a) } } function getLocalStorageTabTextKey() { return getSavedKey($edit_text.attr("name")) } function getSavedKey(a) { return $wiki_form.attr("id") + $wiki_form.attr("name") + a + tab_id } function restoreTabFromDraft() { showDraftToolTip("Tab was restored from draft", 2000); $edit_text.val(getFromStorage(getLocalStorageTabTextKey())); showWikiButtons(); for (var a = 0; a < form_fields_to_save.length; a++) { form_fields_to_save[a].val(getFromStorage(getSavedKey(form_fields_to_save[a].attr("name")))) } detectTabLongLines(); $(document).trigger("wikiTabRestored") } function clearDraft() { removeFromStorage(getLocalStorageTabTextKey()); removeFromStorage(revision_key); hideWikiButtons(); for (var a = 0; a < form_fields_to_save.length; a++) { removeFromStorage(getSavedKey(form_fields_to_save[a].attr("name"))) } } function setAutoFindChords() { $chords_find.bootstrapSwitch("state", false); if ($edit_text.length) { $edit_text.val($edit_text.val().replace(/\[ch\](.+?)\[\/ch\]/g, "$1")) } } function unsetAutoFindChords(a) { $chords_find.bootstrapSwitch("state", true); if ($edit_text.length) { if (a && ($(".js-source-chord-content").length > 0)) { $edit_text.val($(".js-source-chord-content").val()) } else { $.post("/contribution/submit/markChords", { tab_text: $edit_text.val() }, function (b) { $edit_text.val(b) }) } } } var sr_widget = { counter: [] }; var counterContainer = $("#js-find-counter"); sr_widget.find = function () { var a = $edit_text.val(); var b = $("#termSearch").val(); var e = ($("#caseSensitive").attr("checked") == "checked") ? true : false; var c = (b + a).hashCode(); if (e == false) { a = a.toLowerCase(); b = b.toLowerCase() } if (typeof sr_widget.counter[c] == "undefined") { sr_widget.counter[c] = locations(b, a) } var g = ($edit_text.getCursorPosEnd()); var f = a.indexOf(b, g); if ((f != -1) || ((f = a.indexOf(b)) != -1)) { $edit_text.selectRange(f, f + b.length); var d = a.substring(1, f + b.length).match(/(\r\n|\n|\r)/ig).length; $edit_text.scrollTop(d * $edit_text.css("font-size").replace("px", "")); counterContainer.text((sr_widget.counter[c].indexOf(f) + 1) + " of " + sr_widget.counter[c].length) } else { counterContainer.text("Nothing found") } }; sr_widget.findAndReplace = function () { var d = $edit_text.val(); var a = $edit_text.val(); var c = $("#termSearch").val(); var b = $("#termReplace").val(); var e = ($("#caseSensitive").attr("checked") == "checked") ? true : false; var g; if (e == false) { a = a.toLowerCase(); c = c.toLowerCase() } var h = ($edit_text.getCursorPosEnd()); var g = a.indexOf(c, h); var f = ""; if ((g != -1) || ((g = a.indexOf(c)) != -1)) { f = d.substring(0, g) + b + d.substring(g + c.length, d.length); $edit_text.val(f); $edit_text.selectRange(g, g + b.length) } else { counterContainer.text("Nothing found") } }; sr_widget.replaceAll = function () { var d = $edit_text.val(); var a = $edit_text.val(); var c = $("#termSearch").val(); var b = $("#termReplace").val(); var e = ($("#caseSensitive").attr("checked") == "checked") ? true : false; if (e == false) { a = a.toLowerCase(); c = c.toLowerCase() } matches = locations(c, a); for (var f = 0; f < matches.length; f++) { sr_widget.findAndReplace() } counterContainer.text(matches.length + " matches were replaced") }; $.fn.selectRange = function (b, a) { return this.each(function () { if (this.setSelectionRange) { this.focus(); this.setSelectionRange(b, a) } else { if (this.createTextRange) { var c = this.createTextRange(); c.collapse(true); c.moveEnd("character", a); c.moveStart("character", b); c.select() } } }) }; $.fn.getCursorPosEnd = function () { var c = 0; var a = this.get(0); if (document.selection) { a.focus(); var b = document.selection.createRange(); c = b.text.length } else { if (a.selectionStart || a.selectionStart == "0") { c = a.selectionEnd } } return c }; $.fn.pressEnter = function (a) { return this.each(function () { $(this).bind("enterPress", a); $(this).keyup(function (b) { if (b.keyCode == 13) { $(this).trigger("enterPress") } }) }) }; function locations(e, c) { var b = [], d = -1; while ((d = c.indexOf(e, d + 1)) >= 0) { b.push(d) } return b } String.prototype.hashCode = function () { var a = 0; if (this.length == 0) { return a } for (i = 0; i < this.length; i++) { chartt = this.charCodeAt(i); a = ((a << 5) - a) + chartt; a = a & a } return a }; function showWikiButtons() { $(".js-view-diff").show(); $(".js-restore-text").show() } function hideWikiButtons() { $(".js-view-diff").hide(); $(".js-restore-text").hide(); is_tab_text_changed = false } function approveMakeDecision(a, b) { $(".js-approve-state").button("loading"); $.getJSON("/contribution/submit/approveDecision", { id: tab_id, is_approve: a, comment: b }).done(function (c) { $(".js-approve-state").hide(); if (c.result) { var d = "Your vote was acknowledged."; if (c.result == "approved") { d = 'Tab was accepted on UG, you can view it <a href="' + c.url + '">here</a>.' } else { if (c.result == "rejected") { d = "Tab was rejected." } } $(".js-modal-label").text("Thanks for the vote!"); $(".js-modal-body").html(d); $("#modal").modal("show") } }) } function replaceTabSymbolsToSpaces(c, a) { if (a == "auto") { for (var b = 0; b < 60; b++) { var c = c.replace(/(\n|^)(.*?)\t/g, function (g, f, e) { var d = e; d = d.replace(/\[[^\]]+\](.*?)\[\/[^\]]+\]/, "$1"); return f + e + " ".repeat(8 - (d.length % 8)) }); if (c.indexOf("\t") == -1) { break } } return c } else { return c.replace(/\t/g, " ".repeat(parseInt(a))) } } function correctTabText(a) { $.ajax({ url: "/tabmanager/correct.php?json_format=1", type: "POST", data: { filename: $edit_text.val(), correct: 1, }, dataType: "json", success: function (b) { $edit_text.val(b); $(document).trigger("tabRemoteChanged"); if (typeof a == "function") { a() } } }) } function hasLongLines() { var b = false; var a = $("#js-preview-text").text().split("\n"); for (var c = 0; c < a.length; c++) { if (a[c].length > max_line_character_count) { b = true; a[c] = '<span class="bg-info">' + a[c] + "</span>" } } if (b) { $("#js-preview-text").html(a.join("\n")) } return b } String.prototype.repeat = function (a) { return new Array(a + 1).join(this) };