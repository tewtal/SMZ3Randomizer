local socket = require("socket")

local connection
local host = '127.0.0.1'
local port = 65398
local connected = false
local stopped = false
local version = 1
local name = "Unnamed"

local function remapAddr(addr, info)
	if info == 'WRAM' then
		addr = addr + 0x7E0000
	elseif info == 'CARTROM' then
		if addr < 0x400000 then
			addr = addr + 0xC00000
		else
			addr = addr + 0x400000
		end
	elseif info == 'CARTRAM' then
		local bank = (addr / 0x2000) + 0xA0
		local saddr = 0x6000 + (addr % 0x2000)
		addr = bit.bor(bit.lshift(bank, 16), saddr)
	end			
	
	return addr
end

local function onMessage(s)
    local parts = {}
    for part in string.gmatch(s, '([^|]+)') do
        parts[#parts + 1] = part
    end
    if parts[1] == "Read" then
        local adr = tonumber(parts[2])
		local info = parts[4]
		
		adr = remapAddr(adr, info)
		
        local length = tonumber(parts[3])
        local byteRange = memory.readbyterange(adr, length)
        connection:send("{\"data\": [" .. table.concat(byteRange, ",") .. "]}\n")
    elseif parts[1] == "Write" then
        local adr = tonumber(parts[2])
		local info = parts[3]

		adr = remapAddr(adr, info)
		
        for k, v in pairs(parts) do
            if k > 2 then
                memory.writebyte(adr + k - 4, tonumber(v))
            end
        end
	elseif parts[1] == "SetName" then
		name = parts[2]
        print("My name is " .. name .. "!")
    elseif parts[1] == "Message" then
        print(parts[2])
    elseif parts[1] == "Exit" then
        print("Lua script stopped, to restart the script press \"Restart\"")
        stopped = true
    elseif parts[1] == "Version" then
        connection:send("Version|BizHawk|" .. version .. "|\n")
    end
end


local main = function()
    if stopped then
        return nil
    end

    if not connected then
        print('Multibridge LUA r' .. version)
        print('Connecting to Multibridge at ' .. host .. ':' .. port)
        connection, err = socket:tcp()
        if err ~= nil then
            emu.print(err)
            return
        end

        local returnCode, errorMessage = connection:connect(host, port)
        if (returnCode == nil) then
            print("Error while connecting: " .. errorMessage)
            stopped = true
            connected = false
            print("Please press \"Restart\" to try to reconnect to Multibridge, make sure its running")
            return
        end

        connection:settimeout(0)
        connected = true
        print('Connected to Multibridge')
        return
    end
    local s, status = connection:receive('*l')
    if s then
        onMessage(s)
    end
    if status == 'closed' then
        print('Connection to Multibridge is closed')
        connection:close()
        connected = false
        return
    end
end

emu.registerbefore(main)