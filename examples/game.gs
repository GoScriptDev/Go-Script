use "std"
use "random"
use "winapi"

clear()

player = "*"
yabloko = "@"

visota = 20
shirina_b_sten = 20
shirina_s_sten = 18

player_x = 10
player_y = 9
yabloko_x = randint(1, 19)
yabloko_y = randint(1, 17)

var ochki
var fps
var kadrov

def check_player_pos() {
    if (player_x == 20 || player_y == 18 || player_x == 0 || player_y == 0) {
        player_x = 10
        player_y = 9
    }
}

mainloop = ldef () {
    i_input = get_key()

    for i = 0; i <= visota; i++ {
        if i == 0 || i == 20 {
            for j = 0; j <= shirina_b_sten; j++ {
                sout "#"
            }
        }
        else {
            sout "#"
            for c = 0; c <= shirina_s_sten; c++ {
                sout " "
            }
            sout "#"
        }
        sout "\n"
    }

    while (true) {
        sleep(50)

        set_cursor_position(yabloko_x, yabloko_y)
        sout yabloko

	    check_player_pos()

        set_cursor_position(player_x, player_y)
        sout " " + "\b"

        if (yabloko_x == player_x && yabloko_y == player_y) {
            set_cursor_position(yabloko_x, yabloko_y)
            sout " "
            ochki += 1
            yabloko_x = randint(1, 19)
            yabloko_y = randint(1, 17)
        }

        i_input = get_key()

        switch as_var("i_input") {
            case "d" {
                player_x += 1
            }
            case "w" {
                player_y -= 1
            }
            case "s" {
                player_y += 1
            }
            case "a" {
                player_x -= 1
            }
            case "x" {
                exit()
            }
        }

        check_player_pos()

        set_cursor_position(player_x, player_y)
        sout player

        set_cursor_position(10, 21)
        sout ochki
    }
}

mainloop()
