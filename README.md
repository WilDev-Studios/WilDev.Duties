# WilDev.Duties
Unturned plugin to add on and off duty announcements through the OpenMod API

## How to Install
Make sure you are in-game and run this command:
`/openmod install WilDev.Duties`

## Documentation
*Config.yaml*
```yaml
Jobs-List:
  ADMIN
  POLICE
  EMS

Messages-Image-URL: https://example.image.org/images/001.jpg
```

*Translations.yaml*
```yaml
# Everything must have a string value (text with double-quotes around it)
# Everything can be configured with Rich Text, only with < and > though

On-Duty:
  ADMIN: "{Player} is now on duty as an Admin"
  POLICE: "{Player} is now on duty as a Police officer"
  EMS: "{Player} is now on duty as an EMS unit"

Off-Duty: "{Player} is now off duty"

No-Job-Found: "Sorry, but that job does not exist"
No-Current-Job: "Sorry, but you can't go off duty as you have no job"
```

*Parameters*
- Rich Text <>: To add color and text formatting support for in-game text - Must be configured with <>

## Contact Us
### [Discord](https://discord.gg/4Ggybyy87d)
