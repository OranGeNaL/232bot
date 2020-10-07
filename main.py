import telebot
import schedule
import weatherer

bot = telebot.TeleBot('1397104718:AAH_V-casteTN9DNsjT59niEjvZcOMLZNuY')

users_list = []
todo_list = []


def send_everyone(text):
    for i in users_list:
        bot.send_message(i, text)


@bot.message_handler(commands=['start'])
def start_message(message):
    bot.send_message(message.chat.id, 'Привет, я бот 232 комнаты!')
    if not (message.chat.id in users_list):
        users_list.append(message.chat.id)


@bot.message_handler(commands=['help'])
def help_message(message):
    bot.send_message(message.chat.id, '''Привет, вот все мои команды:
    /start - инициализация нового пользователя
    /help - помощь
    /addTODO <дело> - добавляет дело в список
    /getTODO - отправляет список дел всем пользователям''')
    users_list.append(message.chat.id)


@bot.message_handler(commands=['wthr'])
def weather_message(message):
    a = weatherer.check_weather_api(city="Хабаровск")
    bot.send_message(message.chat.id, a[0])
    bot.send_photo(message.chat.id, open(a[1], 'rb'))

bot.polling()