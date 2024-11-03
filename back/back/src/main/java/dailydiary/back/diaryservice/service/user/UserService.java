package dailydiary.back.diaryservice.service.user;
import dailydiary.back.diaryservice.domain.user.User;
import dailydiary.back.diaryservice.repository.user.UserRepository;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import java.util.Optional;
@Slf4j
@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    private BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

    // 사용자 등록
    public User registerUser(String username, String password) {
        String encodedPassword = passwordEncoder.encode(password);
        User user = new User(username, encodedPassword);
        return userRepository.save(user);
    }

    public Optional<User> authenticate(String username, String password) {
        Optional<User> userOpt = userRepository.findByUsername(username);
        if (userOpt.isPresent()) {
            log.info("User found: " + username);  // logger 대신 log 사용
            if (passwordEncoder.matches(password, userOpt.get().getPassword())) {
                log.info("Password matches for user: " + username);
                return userOpt;  // 인증 성공 시 사용자 반환
            } else {
                log.warn("Password mismatch for user: " + username);
            }
        } else {
            log.warn("User not found: " + username);
        }
        return Optional.empty();  // 인증 실패
    }
}