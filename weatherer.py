import requests
import pyowm


def check_weather():
    owm = pyowm.OWM('da5bd0105833e7d94fbfcb03212a19b6')
    mgr = owm.weather_manager()

    observation = mgr.weather_at_place('Khabarovsk')
    w = observation.weather

    res = ""  # "На данный момент:\n"
    res += w.detailed_status + "\n"
    res += "Температура " + str(w.temperature('celsius')['temp']) + "C\n"
    res += "Ощущается как " + str(w.temperature('celsius')['feels_like']) + "C\n"
    res += "Скорость ветра " + str(w.wind()['speed']) + 'м/с\n'
    print(res)
    return res


def check_weather_api(city):
    req = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=da5bd0105833e7d94fbfcb03212a19b6&lang=ru&units=metric"
    resp = requests.get(req).json()

    res = ""  # "На данный момент:\n"
    res += resp['weather'][0]['description'][0].upper() + resp['weather'][0]['description'][1:] + "\n"
    res += "Температура " + str(resp['main']['temp']) + "C\n"
    res += "Ощущается как " + str(resp['main']['feels_like']) + "C\n"
    res += "Скорость ветра " + str(resp['wind']['speed']) + 'м/с\n'

    print(res)
    return res


if __name__ == '__main__':
    check_weather_api("Хабаровск")
