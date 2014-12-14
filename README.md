# Rule Of Firewall

A Windows firewall tool for the lazy and unorganized. Rule your traffic with an iron fist.

### "Features":
* Imports firewall rules into the "Windows Firewall with Advanced Security". Useful for deploying rule packages.
* Natively imports files created from the above MMC console using the "Export List..." Action in the right-hand panel.
* Given a silly name late at night because the developer thought himself clever (Rule of Law -> "Law" backwards is almost "wall" -> Firewall -> Rule of Firewall). Don't mock him too much in public about it.
* Free. And MIT-licensed.
* Simplifies firewall work for those of us working without decent Group Policy manglement. Be it on your own system or a client's.
* Old code from an evening project.
* Not very organized.

### Notes:
* Program exceptions don't import very well when the program isn't present on the machine.
* The windows firewall interface is rather finicky about order, and there's some warnings on the internet of a need to artificially delay property/method access in certain situations (which I haven't found, yet). Please confirm that the rule imported as expected. If not, please create an issue, note the differences, and attach the rule file. If possible, do some digging on your own.
* Use the Group functionality (just set it to a string value). It's surprisingly useful.
* Some example rule files have been included. Feel free to submit more with pull requests for your (least) favorite applications and scenarios.

Not dead? Have a computer? Give the gift of code. Find some sort of pointless-yet-useful code lying around and share it. Join the embarrassment.
